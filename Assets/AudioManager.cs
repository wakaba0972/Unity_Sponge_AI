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
    string text = "�ڡA�ڥ�?�n��A?�A�ڳQ�y�y��F�A�Z?���Ĥ�?���F���A�ڴN�ܤU���F�A?�b�n�h�F�C";
    string postData = "{\r\n  \"text\": \"" + text + "\",\r\n  \"text_lang\": \"zh\",\r\n  \"ref_audio_path\": \"ref_audios\\\\star_ref.wav\",\r\n  \"aux_ref_audio_paths\": [],\r\n  \"prompt_lang\": \"zh\",\r\n  \"prompt_text\": \"�ڥ�?�n��A?�ڥ�?�n��A?�ڥ�?�n��A?�ڡA�ڥ�?�n��A?�A�ڳQ�y�y��F�A�Z?���Ĥ�?���F���A�ڴN�ܤU���F�A?�b�n�h�F�C\",\r\n  \"top_k\": 5,\r\n  \"top_p\": 1,\r\n  \"temperature\": 1,\r\n  \"text_split_method\": \"cut0\",\r\n  \"batch_size\": 1,\r\n  \"batch_threshold\": 0.75,\r\n  \"split_bucket\": true,\r\n  \"speed_factor\": 1,\r\n  \"fragment_interval\": 0.3,\r\n  \"seed\": -1,\r\n  \"media_type\": \"wav\",\r\n  \"streaming_mode\": false,\r\n  \"parallel_infer\": true,\r\n  \"repetition_penalty\": 1.35,\r\n  \"sample_steps\": 32,\r\n  \"super_sampling\": false\r\n}";
    */
    void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("���U�ť���A�}�l�ШD TTS �A��");
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
        Debug.Log("���T�x�s��: " + filePath);

        // 4. ���� WAV�]�q�ɮ׸��J�^
        using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return audioRequest.SendWebRequest();

            if (audioRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("���񥢱�: " + audioRequest.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(audioRequest);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }*/
}
