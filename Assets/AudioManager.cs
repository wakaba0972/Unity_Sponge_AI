using UnityEngine;
using System.Text;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class AudioManager : MonoBehaviour
{
    public bool isRunning = false;
    private string PATH;
    public AudioSource audioSource;
    private string DefaultURL = "http://127.0.0.1:9880/ctts?";

    private void Start()
    {
        PATH = Application.persistentDataPath;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            string URL = DefaultURL + "character=star&text=是不是帳號密碼太臭的原因";
            Debug.Log("按下空白鍵，開始請求 TTS 服務");
            Send(URL);
        }*/
    }

    private ScriptStruct OpenScript(int id)
    {
        Debug.Log($"嘗試讀取劇本: {id}.json");
        string filePath = $"{PATH}/{id}/{id}.json";
        try
        {
            string jsonText = File.ReadAllText(filePath);
            return JsonUtility.FromJson<ScriptStruct>(jsonText);
        }
        catch (Exception e)
        {
            Debug.LogError($"讀取失敗: {e.Message}");
            return null;
        }
    }

    public async Task<int> Request(int Current_TTS_ID, int Current_Script_ID)
    {
        int res = Current_TTS_ID;
        isRunning = true;

        if(Current_TTS_ID < Current_Script_ID)
        {
            Debug.Log($"開始處理TTS, {Current_TTS_ID+1}劇本");
            ScriptStruct scriptText = OpenScript(Current_TTS_ID+1);
            for(int i=0; i < scriptText.script.Length; ++i)
            {
                Line line = scriptText.script[i];
                string character = line.character;
                string text = line.text;
                string URL = $"{DefaultURL}character={character}&text={text}";

                await Handle(URL, Current_TTS_ID+1, i);
            }
            res++;
        }
        isRunning = false;

        return res;
    }

    public async Task Handle(string URL, int Current_Script_ID, int sid)
    {
        Debug.Log($"開始處理TTS台詞, {Current_Script_ID}-{sid}");
        byte[] audioData = await RequestTTS(URL);
        string savePath = $"{PATH}/{Current_Script_ID}/{sid}.wav";
        File.WriteAllBytes(savePath, audioData);

    }

    private async Task<byte[]> RequestTTS(string URL)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            await request.SendWebRequest();
            return request.downloadHandler.data;
        }
    }

    public async Task PlayAudio(string filePath)
    {
        using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            await audioRequest.SendWebRequest();
            AudioClip clip = DownloadHandlerAudioClip.GetContent(audioRequest);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
