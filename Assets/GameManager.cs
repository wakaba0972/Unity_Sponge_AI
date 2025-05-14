using UnityEngine;
using UnityEngine.InputSystem.Android;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;
    public ScriptManager scriptManager;

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
        scriptManager.say();
    }

    void Update()
    {
        
    }
}
