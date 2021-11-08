using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    public float rayDist;
    public Transform startPos, endPos;
    // Start is called before the first frame update

    public void attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(startPos.position,endPos.position,out hit,rayDist))
        {
            if (hit.collider.tag == "Zombie")
            {
                //
            }
        }
    }
}
