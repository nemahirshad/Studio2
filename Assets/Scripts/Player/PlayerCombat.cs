using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Inventory inventory;

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            other.gameObject.GetComponent<breakable>().OnBreak();
            inventory.scoreCount++;
            inventory.UpdateScore();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyInfo>().TakeDamage(damage);
        }
    }
}
