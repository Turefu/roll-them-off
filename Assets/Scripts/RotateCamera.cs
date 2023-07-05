using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed; 
    private float rotateSpeed;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, GetRotateSpeed());
    }

    private float GetRotateSpeed()
    {
        return rotateSpeed = speed * Time.deltaTime * GetHorizontalInput();
    }

    private float  GetHorizontalInput()
    {
        return horizontalInput = Input.GetAxis("Horizontal");
    }

}
