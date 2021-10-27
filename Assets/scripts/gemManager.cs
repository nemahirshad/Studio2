using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemManager : MonoBehaviour
{
    [SerializeField]
    int gemNumber;
    public GameObject gemPrefab;
    public GameObject[] vases;



    // Start is called before the first frame update
    void Start()
    {
        int a = 0;
        while (a < gemNumber)
        {


        }
        for (int i = 0; i < gemNumber; i++)
        {
            int Rand = Random.Range(0, vases.Length);
            vases[Rand].GetComponent<breakable>().hasGem = true;
        }

        
        foreach (var item in vases)
        {
            if (item.GetComponent<breakable>().hasGem == true)
            {
                a++;
            }
        }

        
        while (a < gemNumber)
        {
            foreach (var item in vases)
            {
                if (item.GetComponent<breakable>().hasGem != true)
                {
                    item.GetComponent<breakable>().hasGem = true;
                    a++;
                }
            }
        }
        
        
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
