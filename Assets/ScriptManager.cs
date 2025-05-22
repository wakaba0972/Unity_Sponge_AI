using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using UnityEditor.PackageManager.Requests;
using System.IO;


public class ScriptManager : MonoBehaviour
{
    private string SAVE_PATH;
    private const string URL_SCRIPT  = "http://localhost:3000/scripts";
    private const string URL_MAX = "http://localhost:3000/max";

    private void Start()
    {
        SAVE_PATH = Application.persistentDataPath;
        Debug.Log($"檔案將儲存在 {SAVE_PATH}");
    }

    // 儲存劇本為json
    private void SaveScript(int id, string script)
    {
        try {
            // 建立資料夾 /[id]/
            string dirPath = $"{SAVE_PATH}/{id}";
            Directory.CreateDirectory(dirPath);

            // 儲存劇本
            string fileName = $"{id}.json";
            string filePath = Path.Combine($"{dirPath}/", fileName);
            File.WriteAllText(filePath, script);
            Debug.Log($"儲存劇本於: {filePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"儲存失敗: {e.Message}");
        }
    }

    // 利用Server的/scripts路由獲取劇本文本
    // 最後返回劇本的文本(string)
    private async Task<string> RequestScript(int id)
    {
        try {
            string URL = $"{URL_SCRIPT}?id={id}";
            using (UnityWebRequest request = UnityWebRequest.Get(URL))
            {
                await request.SendWebRequest();
                Debug.Log($"取得{id}.json文本:");

                return request.downloadHandler.text;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"請求失敗: {e.Message}");
            return string.Empty;
        }
    }

    // 獲取MaxID後, 與當前的Script_ID進行比較
    // 請求[Script_ID+1, MAXID]的文本, 並儲存至本地端
    // 最後返回MaxID以更新Script_ID
    public async Task<int> Request(int Current_Script_ID)
    {
        try
        {
            int maxID = await RequestMaxID();
            for (int id = Current_Script_ID + 1; id <= maxID; ++id)
            {
                string script = await RequestScript(id);
                SaveScript(id, script);
            }

            return maxID;
        }
        catch (Exception e)
        {
            Debug.LogError($"請求失敗: {e.Message}");
            return Current_Script_ID;
        }
    }

    // 利用Server的/max路由獲取當前最大的ID
    private async Task<int> RequestMaxID()
    {
        try {
            using (UnityWebRequest request = UnityWebRequest.Get(URL_MAX))
            {
                await request.SendWebRequest();
                Debug.Log("取得MAX_ID " + request.downloadHandler.text);

                return int.Parse(request.downloadHandler.text);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"請求失敗: {e.Message}");
            return 0;
        }
    }
}
