using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gem, health, fuel;

    GameObject obj;

    [SerializeField] GameObject GamPrefab;

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBreak();
            Destroy(gameObject);
        }
    }

    public void OnBreak()
    {
        if (hasGem == true)
        {
            Instantiate(Gem, transform.position, Quaternion.identity);
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
