using UnityEngine;
using System.Text;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    /*string url = "http://192.168.76.19:9880/tts";
    string text = "啊，我本?要跟你?，我被球球到了，后?雪融化?成了水，我就喝下水了，?在好多了。";
    string postData = "{\r\n  \"text\": \"" + text + "\",\r\n  \"text_lang\": \"zh\",\r\n  \"ref_audio_path\": \"ref_audios\\\\star_ref.wav\",\r\n  \"aux_ref_audio_paths\": [],\r\n  \"prompt_lang\": \"zh\",\r\n  \"prompt_text\": \"我本?要跟你?我本?要跟你?我本?要跟你?啊，我本?要跟你?，我被球球到了，后?雪融化?成了水，我就喝下水了，?在好多了。\",\r\n  \"top_k\": 5,\r\n  \"top_p\": 1,\r\n  \"temperature\": 1,\r\n  \"text_split_method\": \"cut0\",\r\n  \"batch_size\": 1,\r\n  \"batch_threshold\": 0.75,\r\n  \"split_bucket\": true,\r\n  \"speed_factor\": 1,\r\n  \"fragment_interval\": 0.3,\r\n  \"seed\": -1,\r\n  \"media_type\": \"wav\",\r\n  \"streaming_mode\": false,\r\n  \"parallel_infer\": true,\r\n  \"repetition_penalty\": 1.35,\r\n  \"sample_steps\": 32,\r\n  \"super_sampling\": false\r\n}";
    */
    void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("按下空白鍵，開始請求 TTS 服務");
            StartCoroutine(ttsRequest(url, postData));
        }*/
    }

   /* IEnumerator ttsRequest(string url, string postData)
    {
        byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(postDataBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.GetResponseHeader("Content-Type"));
        }

        byte[] audioData = request.downloadHandler.data;
        string filePath = Path.Combine(Application.persistentDataPath, "result.wav");
        File.WriteAllBytes(filePath, audioData);
        Debug.Log("音訊儲存於: " + filePath);

        // 4. 播放 WAV（從檔案載入）
        using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return audioRequest.SendWebRequest();

            if (audioRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("播放失敗: " + audioRequest.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(audioRequest);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }*/
}
