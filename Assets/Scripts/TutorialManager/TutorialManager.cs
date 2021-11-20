using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int i;
    private int countForInput;
    public GameObject jumpObj;


    void Start()
    {
       countForInput = 0;
       jumpObj.SetActive(false);
    }

    public void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (this.i == i)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (i == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // INTRO TO SKIP
            {
                i++;
            }
        }
        else if (i == 1) //MOVING AND LOOKING
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                countForInput++;  //1
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                countForInput++;    //2
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                countForInput++; //3
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                countForInput++;   //4
            }
            if (countForInput >= 4)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("worgehdks");
                    i++;
                    jumpObj.SetActive(true);
                }
            }
        }
        else if (i == 2) //JUMP
        {
            if (Input.GetKeyDown(KeyCode.Return ))
            {
                jumpObj.SetActive(false);
                i++;
            }
        }
        else if (i == 3) //RKey
        {
              if (Input.GetKeyDown(KeyCode.Return))
              {
                   i++;
              }
        }
        else if (i == 4) //coMBAT
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        } else if (i == 5) //Inventory
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }else if (i == 6) //End
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                i++;
            }
        }
    }
}
