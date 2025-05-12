using UnityEngine;

public class spinStar : MonoBehaviour
{

    public Vector3 RotateAmount = new Vector3(0.1f, 0, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount);
    }
}
