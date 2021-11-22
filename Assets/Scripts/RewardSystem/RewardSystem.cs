using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RewardSystem : MonoBehaviour
{
    public GameObject rewardSystem;
    public Inventory hs;
    public List<Item> rewardItems = new List<Item>();
    public GameObject claimButton;
    public GameObject closeButton;

    

    public void Awake()
    {
        rewardSystem.SetActive(false);

    }

    public void Update()
    {
        if(hs.scoreCount == 2)
        {
            claimButton.SetActive(true);
            //Time.timeScale = 0.0f * Time.deltaTime;
            rewardSystem.SetActive(true);
        }
    }

   public  void ClaimReward()
    {
        
        if (hs.scoreCount == 2)
        {
            Debug.Log("claimed");
            hs.AddItem(rewardItems[0]);
            hs.SortItems(rewardItems[0]);
            claimButton.SetActive(false);
            hs.scoreCount = 0;
        }

    }
    public void CloseRewardWindow()
    {
        hs.scoreCount = 0;
        rewardSystem.SetActive(false);
    }
}
