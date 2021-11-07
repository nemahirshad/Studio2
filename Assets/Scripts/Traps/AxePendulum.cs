using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePendulum : MonoBehaviour
{
    //Might just make this using Animations instead.
    //Creating a pendulum using physics is looking more and more difficult as time goes on.

    Rigidbody rgbody;

    [SerializeField] float swingSpeed;
    [SerializeField] float swingStartAngle;
    [SerializeField] float swingEndAngle;

    bool swingClockwise;

    void Start()
    {
        rgbody = GetComponent<Rigidbody>();
        swingClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {
        SwingAxe();
    }

    public void ChangeSwingDir()
    {
        if (transform.rotation.z > swingStartAngle)
            swingClockwise = false;

        if (transform.rotation.z < swingEndAngle)
            swingClockwise = true;
    }

    public void SwingAxe()
    {
        ChangeSwingDir();

        
    }
}
