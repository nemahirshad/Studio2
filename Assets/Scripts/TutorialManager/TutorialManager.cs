using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject rewardSystem;
    public GameObject[] popUps;
    public GameObject firstPopUp;
    private int i;
    private int countForInput;

    public bool firstRun;
    public bool interacted;

    void Start()
    {
       countForInput = 0;
    }

    public void Update()
    {
        if (firstRun)
        {
            firstPopUp.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                firstRun = false;
                firstPopUp.SetActive(false);
            }
        }
        else
        {
            if (interacted)
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
                        }
                    }
                }
                else if (i == 2) //JUMP
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
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
                }
                else if (i == 5) //Inventory
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        i++;
                    }
                }
                else if (i == 6) //End
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        i++;
                    }
                }
                else if (i == 7) //End
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        i++;
                        rewardSystem.SetActive(true);
                    }
                }
            }
        }
    }
}
