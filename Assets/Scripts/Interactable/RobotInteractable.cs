using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotInteractable : Interactable
{
    public TutorialManager tutManager;

    public override void Interact()
    {
        tutManager.interacted = true;
    }
}
