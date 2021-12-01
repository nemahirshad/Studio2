using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform targetEntity;
    public float spaceBetween = 1.5f;

    void Update()
    {
        if (Vector3.Distance(targetEntity.position, transform.position) >= spaceBetween)
        {
            Vector3 targetDir = targetEntity.position - transform.position;
            transform.Translate(targetDir * Time.deltaTime);
        }
    }
}