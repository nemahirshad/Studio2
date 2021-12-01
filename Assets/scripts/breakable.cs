using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gem, health, fuel;

    GameObject obj;

   
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnBreak();
        //}
    }

    public void OnBreak()
    {
        if (hasGem == true)
        {
            Instantiate(Gem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        //else
        //{
        //    int x = Random.Range(0, 3);

        //    switch (x)
        //    {
        //        case 0:
        //            //Drop heart
        //            obj = Instantiate(health);
        //            obj.transform.position = transform.position;
        //            Destroy(gameObject);
        //            break;

        //        case 1:
        //            //Drop Fuel
        //            obj = Instantiate(health);
        //            obj.transform.position = transform.position;
        //            Destroy(gameObject);
        //            break;

        //        case 2:
        //            //Drop Nothing
        //            Destroy(gameObject);
        //            break;
        //    }

        //}
    }
}
