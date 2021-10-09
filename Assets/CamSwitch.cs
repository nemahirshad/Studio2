using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    TPController tpControl;
    public GameObject TPCam;
    public GameObject FPCam;
    public bool check;
    public float mouseSense = 5.0f;


    public void Start()
    {
        TPCam.SetActive(true);
        FPCam.SetActive(false);
        check = true;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (check) // WHEN FIRST PERSON IS ENABLED
            {
                TPCam.SetActive(false);
                FPCam.SetActive(true);

            }
            else   // WHEN THIRD PERSON IS ENABLED
            {
                TPCam.SetActive(true);
                FPCam.SetActive(false);
            }
        }
        check = !check;
    }
}
