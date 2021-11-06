using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GemScriptableObject gems;

    public GameObject inventoryObject;
    public GameObject rewardSystem;
    public GameObject followMouseImage;
  

    public Slot[] slots;
    private void Start()
    {
        inventoryObject.SetActive(false);
        followMouseImage.SetActive(false);
        foreach (Slot i in slots)
        {
            i.CustomStart();
        }
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
            
        }
        if(inventoryObject.activeInHierarchy || rewardSystem.activeInHierarchy)
            {
            followMouseImage.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined; } 
        else { followMouseImage.SetActive(false); Cursor.lockState = CursorLockMode.Locked; }

        foreach (Slot i in slots)
            i.CheckForItem();
    }

    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {
        int amountInStack = itemToBeAdded.amountInStack;
        List<Item> stackableItems = new List<Item>();
        List<Slot> emptySlots = new List<Slot>();

        if (startingItem && startingItem.itemID == itemToBeAdded.itemID && startingItem.amountInStack < startingItem.maxStackSize)
            stackableItems.Add(startingItem);

        foreach (Slot i in slots) // looking at Slots in Slot
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == itemToBeAdded.itemID && z.amountInStack < z.maxStackSize && z != startingItem)
                    stackableItems.Add(z);
            }
            else
            {
                emptySlots.Add(i);
            }
        }

        foreach (Item i in stackableItems) //lookiing at item in stackable items
        {
            int amountThatCanBeAdded = i.maxStackSize - i.amountInStack;
            if (amountInStack <= amountThatCanBeAdded)
            {
                i.amountInStack += amountInStack;
                Destroy(itemToBeAdded.gameObject);
                return;
            }
            else
            {
                i.amountInStack = i.maxStackSize;
                amountInStack -= amountThatCanBeAdded;
            }
        }

        itemToBeAdded.amountInStack = amountInStack;
        if (emptySlots.Count > 0)
        {
            itemToBeAdded.transform.parent = emptySlots[0].transform;
            itemToBeAdded.gameObject.SetActive(false);
        }
    }
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Item>())
            AddItem(col.GetComponent<Item>());

        if (col.CompareTag("Gem"))
        {
            gems.value++;
        }
    }
}
