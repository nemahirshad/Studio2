using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    public bool hasGem;
<<<<<<< HEAD
<<<<<<< HEAD
    [SerializeField] GameObject Gem, health, fuel;

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }
=======
    [SerializeField] GameObject GamPrefab;
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15
=======
    [SerializeField] GameObject GamPrefab;
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onBreak();
            Destroy(gameObject);
        }
    }

    public void OnBreak()
    {
        if(hasGem == true)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            Instantiate(Gem,transform.position,Quaternion.identity);
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
=======
            Instantiate(GamPrefab,transform.position,Quaternion.identity);
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15
=======
            Instantiate(GamPrefab,transform.position,Quaternion.identity);
>>>>>>> 7d48e6f7538afef6f46ca19b2d41d13f6ba80f15
        }
    }
}
