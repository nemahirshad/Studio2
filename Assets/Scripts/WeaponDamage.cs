using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;
    public float rayDist;
    public Transform startPos, endPos;
    
    // Start is called before the first frame update

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(startPos.position,endPos.position,out hit,rayDist))
        {
            if (hit.collider.tag == "Golem")
            {
                hit.collider.GetComponent<Brendan.Golams_Ai>().OnDeathEffects();
                Debug.Log("GOLEM HIT");
            }
            if (hit.collider.tag == "Breakable")
            { 
                hit.collider.GetComponent<breakable>().OnBreak();
                Debug.Log("Vase Break");
            }
            //if (hit.collider.tag == "Golem")
            //{
            //    hit.collider.GetComponent<Brendan.Golams_Ai>().onDeathEffects();
            //    Debug.Log("GOLEM HIT");
            //}

        }
    }
}
