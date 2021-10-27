using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gam;

    // Update is called once per frame
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
            Instantiate(Gam,transform.position,Quaternion.identity);
        }
    }
}
