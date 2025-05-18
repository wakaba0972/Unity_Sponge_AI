using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioManager audioManager;
    public ScriptManager scriptManager;

    private GameObject sponge;
    private GameObject star;
    private GameObject squidward;
    private GameObject krab;


    void Awake()
    {
        // ³æ¨Ò³B²z
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
        sponge = GameObject.Find("Spongebob wrapper");
        star = GameObject.Find("Patrick wrapper");
        squidward = GameObject.Find("Squidward wrapper");
        krab = GameObject.Find("Krab wrapper");
    }

    void Update()
    {

    }

    private void  New_Round()
    {

    }
}
