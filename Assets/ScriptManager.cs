using UnityEngine;
using System.Text;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class ScriptManager : MonoBehaviour
{
    private int Counter;
    private string URL = "http://localhost:3000/scripts";

    void Start()
    {
        // 從 PlayerPrefs 讀取Counter, 用作獲取劇本的編號
        Counter = PlayerPrefs.GetInt("Counter", 0);
    }

    void Update()
    {

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Counter", Counter);
    }

    public void say()
    {
        Debug.Log("按下空白鍵，開始請求 TTS 服務");
    }
}
