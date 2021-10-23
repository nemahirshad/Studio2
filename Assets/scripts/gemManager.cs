using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemManager : MonoBehaviour
{
    
    public GameObject[] gems;
    // Start is called before the first frame update
    void Start()
    {
        int Rond = Random.Range(0, gems.Length);
        gems[Rond].GetComponent<breakable>().hasGem = true;
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
