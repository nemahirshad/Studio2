using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.LogError("Interactable " + transform.name +" not overridden");
    }
}