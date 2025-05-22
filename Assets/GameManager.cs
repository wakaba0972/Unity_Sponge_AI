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

    // �w�������@����(ID)
    private int Executable_ID;

    // �w�x�s�奻���@����(ID)
    private int Script_ID;

    // �w�x�sTTS���@����(ID)  
    private int TTS_ID;

    // �Ω󰱤�ШD���лx
    private bool Stop = false;


    void Awake()
    {
        // ��ҳB�z
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
        // ������⪺GameObject�A��K����
        sponge = GameObject.Find("Spongebob wrapper");
        star = GameObject.Find("Patrick wrapper");
        squidward = GameObject.Find("Squidward wrapper");
        krab = GameObject.Find("Krab wrapper");

        // �q PlayerPrefs Ū��Counter, �Χ@����@�����s��
        Executable_ID = PlayerPrefs.GetInt("Executable_ID", 0);
        Script_ID = PlayerPrefs.GetInt("Script_ID", 0);
        TTS_ID = PlayerPrefs.GetInt("TTS_ID", 0);

        // �I������Script_Request_Loop()
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
            Debug.Log("����Script Request!");

            // ��sScript_ID, �VServer�ШD�@�����x�s�ܥ��a��
            Script_ID = await scriptManager.Request(Script_ID);

            // �C10��ШD�@��
            await Task.Delay(10000);
        }
    }
    
    private void OnDestroy()
    {
        /*// �{���������x�s��e��Counter
        PlayerPrefs.SetInt("Executable_ID", Executable_ID);
        PlayerPrefs.SetInt("Script_ID", Script_ID);
        PlayerPrefs.SetInt("TTS_ID", TTS_ID);*/

        // ���ե�
        PlayerPrefs.SetInt("Executable_ID", 0);
        PlayerPrefs.SetInt("Script_ID", 0);
        PlayerPrefs.SetInt("TTS_ID", 0);

        // ����ШD
        Stop = true;
    }
}
