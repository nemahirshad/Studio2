using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Brendan 
{
    public class WeaponDamage : MonoBehaviour
    {
        public int damage;
        public float rayDist;
        public Transform startPos, endPos;
        public LayerMask mask;


        public void Attack()
        {
            RaycastHit hit;
            if (Physics.Raycast(startPos.position, endPos.position, out hit, rayDist))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Breakable")
                {
                    hit.collider.GetComponent<Breakable>().OnBreak();
                    Debug.Log("Vase Break");

                }
                //if (hit.collider.tag == "Golem")
                //{
                //    hit.collider.GetComponent<Brendan.Golams_Ai>().onDeathEffects();
                //    Debug.Log("GOLEM HIT");
                //}



            }
        }

        public void OnDrawGizmos()
        {

            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPos.position, endPos.position);
        }
    }
}
