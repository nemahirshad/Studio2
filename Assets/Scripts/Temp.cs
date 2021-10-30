using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public bool hasGem;
    [SerializeField] GameObject Gem, health, fuel;

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
