using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraControler : MonoBehaviour
{

    public Transform target;        // �n���H������
    public Vector3 offset;          // �۾��P���⪺�۹��m�]�����q�^
    public float smoothSpeed = 0.125f;  // ���Ƴt��

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

        transform.LookAt(target); // �p�G�A�Ʊ����Y�ݵۨ�
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
