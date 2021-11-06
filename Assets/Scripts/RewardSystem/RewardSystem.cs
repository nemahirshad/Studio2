using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RewardSystem : MonoBehaviour
{
    public GameObject rewardSystem;
    public int score;
    public int highScore;
   

    public void Awake()
    {
        //rewardSystem.SetActive(false);
    }

    public void Update()
    {
        if(score >= highScore)
        {
            //Time.timeScale = 0.0f * Time.deltaTime;
            rewardSystem.SetActive(true);
        }
    }

   public  void ClaimReward()
    {
            Debug.Log("claimed");
           // Add Texture to Player
    }
    public void CloseRewardWindow()
    {
           
        if(rewardSystem != null)
        {
            rewardSystem.SetActive(false);
        }
    }
}
