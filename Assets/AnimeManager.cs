using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Member;

public class AnimeManager : MonoBehaviour
{
    public bool isRunning = false;
    public AudioManager audioManager;
    public CinemachineCamera[] vcams;
    public TextMeshProUGUI subTitles; 
    public TextMeshProUGUI Topic;

    private string PATH;
    public GameObject sponge;
    public GameObject star;
    public GameObject squidward;
    public GameObject krab;
    public GameObject sandy;

    public GameObject Sponge_HeadPoint;
    public GameObject Star_HeadPoint;
    public GameObject Squid_HeadPoint;
    public GameObject Krab_HeadPoint;
    public GameObject Sandy_HeadPoint;

    private Coroutine Sponge_Coroutine;
    private Coroutine Star_Coroutine;
    private Coroutine Squid_Coroutine;
    private Coroutine Krab_Coroutine;
    private Coroutine Sandy_Coroutine;

    private void Start()
    {
        PATH = Application.persistentDataPath;
    }

    private void init()
    {
        sponge.transform.position = new Vector3(-1, 2, -11);
        star.transform.position = new Vector3(4, 2, -20);
        squidward.transform.position = new Vector3(5, 2, -8);
        krab.transform.position = new Vector3(10, 2, -16);
        sandy.transform.position = new Vector3(-5, 2, -19);
    }

    private IEnumerator LookRotation(GameObject source, GameObject target, float speed = 2f)
    {
        while (true)
        {
            Vector3 direction = target.transform.position - source.transform.position;
            direction.y = 0f; // 水平旋轉，忽略上下

            if (direction.sqrMagnitude < 0.001f)
                yield break; // 避免 zero vector 錯誤

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 如果角度非常接近就結束
            if (Quaternion.Angle(source.transform.rotation, targetRotation) < 0.5f)
            {
                source.transform.rotation = targetRotation;
                yield break;
            }

            // 插值旋轉
            source.transform.rotation = Quaternion.Lerp(source.transform.rotation, targetRotation, Time.deltaTime * speed);
            yield return null;
        }
    }
    Coroutine RandMove(GameObject a, GameObject b) 
    {
        return StartCoroutine(LookRotation(a, b));
        //if (UnityEngine.Random.Range(0f, 1f) < 0.8f) return StartCoroutine(LookRotation(a, b));
        //else return StartCoroutine(WalkAround(a));
    }

    private ScriptStruct OpenScript(int id)
    {
        Debug.Log(PATH);
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

    public async Task<int> Play(int Played_ID, int TTS_ID)
    {
        isRunning = true;
        int res = Played_ID;

        //Debug.Log($"當前已播放劇本數: {Played_ID}，TTS劇本數: {TTS_ID}");

        if (Played_ID < TTS_ID)
        {
            Debug.Log($"開始播放劇本: {Played_ID + 1}");
            ScriptStruct scriptJson = OpenScript(Played_ID + 1);

            Topic.text = $"{Played_ID + 1}. {scriptJson.topic}";

            float angle = UnityEngine.Random.Range(0f, 359f);

            vcams[0].GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = angle;
            vcams[1].GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value = angle;

            init();

            for (int i = 0; i < scriptJson.script.Length; ++i)
            {
                Line line = scriptJson.script[i];
                string character = line.character;
                string text = line.text;

                vcams[i&1].Priority = 10;
                vcams[1-(i&1)].Priority = 0;

                if (Sponge_Coroutine != null) StopCoroutine(Sponge_Coroutine);
                if (Star_Coroutine != null) StopCoroutine(Star_Coroutine);
                if (Squid_Coroutine != null) StopCoroutine(Squid_Coroutine);
                if (Krab_Coroutine != null) StopCoroutine(Krab_Coroutine);
                if (Sandy_Coroutine != null) StopCoroutine(Sandy_Coroutine);

                switch (character)
                {
                    case "海綿寶寶":
                        vcams[i&1].Target = new CameraTarget { TrackingTarget = Sponge_HeadPoint.transform };
                        Star_Coroutine = RandMove(star, sponge);
                        Squid_Coroutine = RandMove(squidward, sponge);
                        Krab_Coroutine = RandMove(krab, sponge);
                        Sandy_Coroutine = RandMove(sandy, sponge);
                        break;
                    case "派大星":
                        vcams[i&1].Target = new CameraTarget { TrackingTarget = Star_HeadPoint.transform };
                        Sponge_Coroutine = RandMove(sponge, star);
                        Squid_Coroutine = RandMove(squidward, star);
                        Krab_Coroutine = RandMove(krab, star);
                        Sandy_Coroutine = RandMove(sandy, star);
                        break;
                    case "章魚哥":
                        vcams[i&1].Target = new CameraTarget { TrackingTarget = Squid_HeadPoint.transform };
                        Sponge_Coroutine = RandMove(sponge, squidward);
                        Star_Coroutine = RandMove(star, squidward);
                        Krab_Coroutine = RandMove(krab, squidward);
                        Sandy_Coroutine = RandMove(sandy, squidward);
                        break;
                    case "蟹老闆":
                        vcams[i&1].Target = new CameraTarget { TrackingTarget = Krab_HeadPoint.transform };
                        Sponge_Coroutine = RandMove(sponge, krab);
                        Squid_Coroutine = RandMove(squidward, krab);
                        Star_Coroutine = RandMove(star, krab);
                        Sandy_Coroutine = RandMove(sandy, krab);
                        break;
                    case "珊迪":
                        vcams[i & 1].Target = new CameraTarget { TrackingTarget = Sandy_HeadPoint.transform };
                        Sponge_Coroutine = RandMove(sponge, sandy);
                        Squid_Coroutine = RandMove(squidward, sandy);
                        Star_Coroutine = RandMove(star, sandy);
                        Krab_Coroutine = RandMove(krab, sandy);
                        break;

                    default:
                        break;
                }

                subTitles.text = text;

                await audioManager.PlayAudio($"{PATH}/{Played_ID + 1}/{i}.wav");

                await Task.Delay(1800);
            }

            res++;
        }

        isRunning = false;
        return res;
    }
}
