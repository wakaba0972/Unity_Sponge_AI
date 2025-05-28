using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;
    public ScriptManager scriptManager;
    public AnimeManager animeManager;

    private GameObject sponge;
    private GameObject star;
    private GameObject squidward;
    private GameObject krab;

    // 已播放的劇本數(ID)
    private int Played_ID;

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

        // 從 PlayerPrefs 讀取Counter, 用作獲取劇本的編號
        Played_ID = PlayerPrefs.GetInt("Played_ID", -1);
        Script_ID = PlayerPrefs.GetInt("Script_ID", -1);
        TTS_ID = PlayerPrefs.GetInt("TTS_ID", -1);

        Debug.Log($"當前Played_ID: {Played_ID}");
        Debug.Log($"當前Script_ID: {Script_ID}");
        Debug.Log($"當前TTS_ID: {TTS_ID}");

        // 背景執行Script_Request_Loop()
        _ = Script_Request_Loop();
        _ = TTS_Request_Loop();
        _ = Anime_Loop();
    }

    private async Task Anime_Loop()
    {
        await Task.Delay(1000);
        while (!Stop)
        {
            // 播放動畫
            Played_ID = await animeManager.Play(Played_ID, TTS_ID);
            await Task.Delay(1000);
        }
    }

    private async Task TTS_Request_Loop()
    {
        await Task.Delay(1000);
        while (!Stop)
        {
            // 更新Script_ID, 向Server請求劇本並儲存至本地端
            TTS_ID = await audioManager.Request(TTS_ID, Script_ID);
            await Task.Delay(1000);
        }
    }

    private async Task Script_Request_Loop() {
        await Task.Delay(1000);
        while (!Stop)
        {
            //Debug.Log("執行Script Request!");

            // 更新Script_ID, 向Server請求劇本並儲存至本地端
            Script_ID = await scriptManager.Request(Script_ID);

            // 每1秒請求一次
            await Task.Delay(1000);
        }
    }
    
    private void OnDestroy()
    {
        // 程式結束時儲存當前的Counter
        //PlayerPrefs.SetInt("Played_ID", Played_ID);
        PlayerPrefs.SetInt("Script_ID", Script_ID);
        PlayerPrefs.SetInt("TTS_ID", TTS_ID);

        // 測試用
        PlayerPrefs.SetInt("Played_ID", -1);
        //PlayerPrefs.SetInt("Script_ID", -1);
        //PlayerPrefs.SetInt("TTS_ID", -1);

        // 中止請求
        Stop = true;
    }
}
