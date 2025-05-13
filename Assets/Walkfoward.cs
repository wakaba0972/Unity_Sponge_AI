using UnityEngine;

public class Walkfoward : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, -5) * Time.deltaTime);
    }
}
