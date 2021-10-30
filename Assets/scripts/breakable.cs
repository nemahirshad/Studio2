using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject GamPrefab;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onBreak();
            Destroy(gameObject);
        }
    }

    void onBreak()
    {
        if(hasGem == true)
        {
            Instantiate(GamPrefab,transform.position,Quaternion.identity);
        }
    }
}
