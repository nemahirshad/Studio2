using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSense = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // mouselook

        float rotLeftToRight = Input.GetAxis("Mouse X") * mouseSense;
        transform.Rotate(0, rotLeftToRight, 0);
    }
}
