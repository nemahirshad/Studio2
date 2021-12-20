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
}
