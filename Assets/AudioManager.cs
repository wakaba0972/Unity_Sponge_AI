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
    public AudioSource audioSource;
    private string DefaultURL = "http://127.0.0.1:9880/ctts?";

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            string URL = DefaultURL + "character=star&text=�O���O�b���K�X�ӯ䪺��]";
            Debug.Log("���U�ť���A�}�l�ШD TTS �A��");
            Send(URL);
        }
    }

    public async void Send(string URL)
    {
        byte[] audioData = await RequestTTS(URL);
        string path = SaveAudio(audioData);
        await PlayAudio(path);
        Debug.Log("���T�x�s��: " + path);
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

    private string SaveAudio(byte[] audioData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "result.wav");
        File.WriteAllBytes(filePath, audioData);
        return filePath;
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
