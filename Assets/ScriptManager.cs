using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;


public class ScriptManager : MonoBehaviour
{
    private int MAX_ID;
    private int Counter;
    private string URL_SCRIPT  = "http://localhost:3000/scripts";
    private string URL_MAX = "http://localhost:3000/max";

    void Start()
    {
        // 從 PlayerPrefs 讀取Counter, 用作獲取劇本的編號
        Counter = PlayerPrefs.GetInt("Counter", 0);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Counter", Counter);
    }

    public async void say()
    {
        Debug.Log(await RequestMaxID());
    }

    // 呼叫RequestMaxID()獲取當前最大的ID並設定為MAX_ID
    public async void SetMaxID()
    {
        MAX_ID = await RequestMaxID();
    }

    public void RequestScript()
    {

    }

    // 我放棄coroutine啦!!!
    // 利用Server的/max路由獲取當前最大的ID
    private async Task<int> RequestMaxID()
    {

        using (UnityWebRequest request = UnityWebRequest.Get(URL_MAX))
        {
            await request.SendWebRequest();
            return int.Parse(request.downloadHandler.text);
        }
    }
}
