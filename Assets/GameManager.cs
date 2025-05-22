using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;
    public ScriptManager scriptManager;

    private GameObject sponge;
    private GameObject star;
    private GameObject squidward;
    private GameObject krab;

    // 已完成的劇本數(ID)
    private int Executable_ID;

    // 已儲存文本的劇本數(ID)
    private int Script_ID;

    // 已儲存TTS的劇本數(ID)  
    private int TTS_ID;

    // 用於停止請求的標誌
    private bool Stop = false;


    void Awake()
    {
        // 單例處理
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // 獲取角色的GameObject，方便控制
        sponge = GameObject.Find("Spongebob wrapper");
        star = GameObject.Find("Patrick wrapper");
        squidward = GameObject.Find("Squidward wrapper");
        krab = GameObject.Find("Krab wrapper");

        // 從 PlayerPrefs 讀取Counter, 用作獲取劇本的編號
        Executable_ID = PlayerPrefs.GetInt("Executable_ID", 0);
        Script_ID = PlayerPrefs.GetInt("Script_ID", 0);
        TTS_ID = PlayerPrefs.GetInt("TTS_ID", 0);

        // 背景執行Script_Request_Loop()
        _ = Script_Request_Loop();
    }

    void Update()
    {

    }

    private void  New_Round()
    {

    }

    private async Task Script_Request_Loop() {
        while (!Stop)
        {
            Debug.Log("執行Script Request!");

            // 更新Script_ID, 向Server請求劇本並儲存至本地端
            Script_ID = await scriptManager.Request(Script_ID);

            // 每10秒請求一次
            await Task.Delay(10000);
        }
    }
    
    private void OnDestroy()
    {
        /*// 程式結束時儲存當前的Counter
        PlayerPrefs.SetInt("Executable_ID", Executable_ID);
        PlayerPrefs.SetInt("Script_ID", Script_ID);
        PlayerPrefs.SetInt("TTS_ID", TTS_ID);*/

        // 測試用
        PlayerPrefs.SetInt("Executable_ID", 0);
        PlayerPrefs.SetInt("Script_ID", 0);
        PlayerPrefs.SetInt("TTS_ID", 0);

        // 中止請求
        Stop = true;
    }
}
