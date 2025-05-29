using UnityEngine;
using System.IO;
using System;
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

        try
        {
            if (Current_TTS_ID < Current_Script_ID)
            {
                Debug.Log($"開始處理TTS, {Current_TTS_ID + 1}劇本");
                ScriptStruct scriptText = OpenScript(Current_TTS_ID + 1);
                for (int i = 0; i < scriptText.script.Length; ++i)
                {
                    Line line = scriptText.script[i];
                    string character = line.character;
                    string text = line.text;
                    string URL = $"{DefaultURL}character={character}&text={text}";

                    await Handle(URL, Current_TTS_ID + 1, i);
                }
                res++;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"處理TTS失敗: {e.Message}");
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
        Debug.Log($"開始播放音頻: {filePath}");
        using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            await audioRequest.SendWebRequest();
            AudioClip clip = DownloadHandlerAudioClip.GetContent(audioRequest);
            audioSource.clip = clip;
            audioSource.Play();
        }

        await WaitForAudioToFinish();
    }

    private async Task WaitForAudioToFinish()
    {
        // 等待直到播放完（播放中 && clip 沒結束）
        while (audioSource.isPlaying)
        {
            await Task.Yield();
        }
    }
}
