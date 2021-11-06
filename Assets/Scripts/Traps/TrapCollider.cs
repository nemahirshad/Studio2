using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    [SerializeField] HealthSystem hpSystem;

    private void OnTriggerEnter(Collider trapCollider)
    {
        if (trapCollider.gameObject.tag == "Player")
        {
            Debug.Log("Player Lost 1 Heart");
            hpSystem.LoseHP(1);
        }
    }
}
