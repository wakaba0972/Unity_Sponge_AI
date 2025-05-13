using UnityEngine;
using UnityEngine.InputSystem.Android;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private AudioManager audioManager;
    private ScriptManager scriptManager;

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

        // ��� AudioManager �M ScriptManager
        audioManager = transform.Find("AudioManager")?.GetComponent<AudioManager>();
        scriptManager = transform.Find("ScriptManager")?.GetComponent<ScriptManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
