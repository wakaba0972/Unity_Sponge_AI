using UnityEngine;
using System.Text;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class Scriptloader : MonoBehaviour
{
    private void Start()
    {
        //string URL = "http://localhost:3000/scripts";
    }

    void Update()
    {

    }

   /* IEnumerator scriptRequest(string url, string postData)
    {
        
    }*/

    public void say()
    {
        Debug.Log("按下空白鍵，開始請求 TTS 服務");
    }
}
