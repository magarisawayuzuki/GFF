using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 3.0f;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("down"))
        {
            transform.position -= transform.up * speed * Time.deltaTime;
        }
        if (Input.GetKey("right"))
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("left"))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }
}
