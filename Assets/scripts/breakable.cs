using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gem, health, fuel, coin;

    GameObject obj;
  

    public void OnBreak()
    {
        if(coin != null)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        Debug.Log("attack");
        if (hasGem == true)
        {
            obj = Instantiate(Gem);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            int x = Random.Range(0, 3);

        switch (x)
            {
                case 0:
                    //Drop heart
                    obj = Instantiate(health);
                    obj.transform.position = transform.position;
                    Destroy(gameObject);
                    break;

                case 1:
                    //Drop Fuel
                    obj = Instantiate(health);
                    obj.transform.position = transform.position;
                    Destroy(gameObject);
                    break;

                case 2:
                    //Drop Nothing
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
