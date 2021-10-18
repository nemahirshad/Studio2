using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onBreak()
    {
        if(hasGem == true)
        {
            Instantiate(Gam,transform.position,Quaternion.identity);
        }
    }
}
