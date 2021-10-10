using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSense = 100.0f;
    public Transform playerBod;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // mouselook
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        playerBod.Rotate(Vector3.up * mouseX);
    }
}
