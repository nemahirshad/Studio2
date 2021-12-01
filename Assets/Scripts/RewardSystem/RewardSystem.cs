using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class RewardSystem : MonoBehaviour
{
    public Inventory hs;
    public List<Item> rewardItems = new List<Item>();
    public GameObject claimButton;
    public GameObject rewardSlot;

    public int prevHighScore;

    /*public void Update()
    {
        Have the reward system give a reward at the end of a level.



        if (hs.highScore == 5)
        {
            hs.highScore = initialHighScore;
        }

        if(hs.highScore>=prevHighScore)
        {
            if (prevHighScore == 0)
            {
                hs.highScore = prevHighScore;
                Debug.Log("First High Score");
                rewardSystem.SetActive(true);
                claimButton.SetActive(true);
                //Time.timeScale = 0.0f * Time.deltaTime;
            }

            else if(hs.highScore >= (prevHighScore * 3) / 2)
            {
                Debug.Log("New High Score");
                rewardSystem.SetActive(true);
                claimButton.SetActive(true);
                //Time.timeScale = 0.0f * Time.deltaTime;
            }
        }
    }*/

   public  void ClaimReward()
    {
        
        if (hs.highScore >= 15)
        {
            Debug.Log("claimed");
            hs.AddItem(rewardItems[0]);
            hs.SortItems(rewardItems[0]);

            foreach (Slot i in hs.slots)
                i.CheckForItem();

            claimButton.SetActive(false);
            rewardSlot.SetActive(false);
            hs.scoreCount = 0;
        }
   }

   /*public void CloseRewardWindow()
    {
        hs.scoreCount = 0;
        //rewardSystem.SetActive(false);
    }*/
}
