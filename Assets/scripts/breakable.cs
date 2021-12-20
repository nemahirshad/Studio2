using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gem, health, fuel, coin;

    GameObject obj;


    public void OnBreak()
    {
        if (coin != null)
        {
            Vector3 coinPosOffset = new Vector3(transform.position.x - 2, transform.position.y + 1, transform.position.z - 1);

            Instantiate(coin, coinPosOffset, Quaternion.identity);
        }
        Debug.Log("attack");
        if (hasGem == true)
        {
            Vector3 gemPosOffset = new Vector3(transform.position.x - 2, transform.position.y + 0.3f, transform.position.z - 1);

            obj = Instantiate(Gem);
            obj.transform.position = gemPosOffset;
            Destroy(gameObject);
        }
        else
        {
            int x = Random.Range(0, 3);

            Vector3 pickupPosOffset = new Vector3(transform.position.x - 2, transform.position.y + 0.3f, transform.position.z - 1);

            switch (x)
            {
                case 0:
                    //Drop heart
                    obj = Instantiate(health);
                    obj.transform.position = pickupPosOffset;
                    Destroy(gameObject);
                    break;

                case 1:
                    //Drop Fuel
                    obj = Instantiate(health);
                    obj.transform.position = pickupPosOffset;
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
