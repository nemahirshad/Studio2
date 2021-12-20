using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Application.Quit(0);
    }
}
