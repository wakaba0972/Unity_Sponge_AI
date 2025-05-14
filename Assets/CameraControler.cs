using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraControler : MonoBehaviour
{

    public Transform target;        // 要跟隨的角色
    public Vector3 offset;          // 相機與角色的相對位置（偏移量）
    public float smoothSpeed = 0.125f;  // 平滑速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LateUpdate();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // 如果你希望鏡頭看著角
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
