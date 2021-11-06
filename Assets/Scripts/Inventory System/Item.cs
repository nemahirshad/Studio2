using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemSprite;

    public int amountInStack = 1;

    public int maxStackSize = 1000;

    public int itemID;

    //----------Farhan's Code----------

    public HealthSystem healthSystem;
    public Inventory inventory;

    //Gonna create 3 bools that we can check off to have the item do different things depending on what's enabled.
    [SerializeField] bool isHealthDrop;
    [SerializeField] bool isTorchFluid;
    [SerializeField] bool isShipFuel;
    //These bools can be set to false when the player inventory does not contain any item corresponding to their respective bools.

    public void UseItem()
    {
        foreach (Slot itemSlot in inventory.slots)
        {
            Item pickup = itemSlot.slotsItem;

            if (pickup.itemID == 1)
            {
                //Maybe add this item to a list of Health Items to keep track of them?
                //Might be unnecessary. Check with Dylan.

                if (Input.GetKeyDown(KeyCode.H))
                {
                    healthSystem.GainHP(1);
                }
            }

            if (pickup.itemID == 2)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    //Insert torch fluid function here. It's supposed to function like a reload mechanic,
                    //where you essentially re-light the torch so that you can use it again.
                }
            }
                
            if (pickup.itemID == 3)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //Insert Ship Fuel function here.
                    //Technically, this drop shouldn't be a consumable,
                    //but perhaps it can be used like to charge a powerful weapon for one time use maybe.
                    //Alternatively, it could be used for a different kind of consumable.
                }
            }
        }
    }
}