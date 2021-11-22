using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistancing : MonoBehaviour
{
    GameObject[] flockEntities;
    public float spaceBetween = 1.5f;

    void Start()
    {
        flockEntities = GameObject.FindGameObjectsWithTag("FlockEntities");
    }

    void Update()
    {
        foreach (GameObject gObj in flockEntities)
        {
            if (gObj != gameObject)
            {
                float distance = Vector3.Distance(gObj.transform.position, this.transform.position);
                if (distance <= spaceBetween)
                {
                    Vector3 direction = transform.position - gObj.transform.position;
                    transform.Translate(direction * Time.deltaTime);
                }
            }
        }
    }
}
