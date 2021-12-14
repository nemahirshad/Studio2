using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GemScriptableObject gems;

    public GameObject inventoryObject;
    public GameObject followMouseImage;

    public PlayerData data;

    public Slot[] slots;

    //-------------------------Farhan's Code-------------------------
    public HealthSystem healthSystem;
    //public PlayerInfo playerInfo;

    public GameObject winScreen;
    public Text scoreText;
    public Text highscoreText;
    public Text finalHighScore;

    public List<Item> healthPickup;
    public List<Item> shipfuelPickup;

    public int scoreCount;
    public int highScore;

    //These bools can be set to false when the player inventory does not contain any item corresponding to their respective bools.
    bool canUseHealth = false;
    bool canUseShipFuel = false;
    //-------------------------Farhan's Code-------------------------


    void Start()
    {
        data.LoadInventory(this);
        //-------------------------Farhan's Code-------------------------
        //Im having some trouble figuring out how to simply use the maxStackSize value from the Item Script
        //So Im gonna leave it empty for now. Will need to work with Dylan to solve this later.
        healthPickup = new List<Item>();
        shipfuelPickup = new List<Item>();

        scoreCount = 0;
        highScore = 0;
        UpdateScore();
        //-------------------------Farhan's Code-------------------------

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

        if(inventoryObject.activeInHierarchy || winScreen.activeInHierarchy)
        {
            followMouseImage.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined; 
        }
        
        else { followMouseImage.SetActive(false); Cursor.lockState = CursorLockMode.Locked; }

        foreach (Slot i in slots)
            i.CheckForItem();

        //-------------------------Farhan's Code-------------------------
        if (Input.GetKeyDown(KeyCode.L))    //For testing Win State
        {
            if (scoreCount >= highScore)
                EndGame();
        }

        if (canUseHealth)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                healthSystem.GainHP(1);
                healthPickup.RemoveAt(healthPickup.Count-1);

                if (healthPickup.Count == 0)
                    canUseHealth = false;
            }
        }

        if (canUseShipFuel)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Insert Ship Fuel function here.
                //Technically, this drop shouldn't be a consumable,
                //but perhaps it can be used like to charge a powerful weapon for one time use maybe.
                //Alternatively, it could be used for a different kind of consumable.

                shipfuelPickup.RemoveAt(shipfuelPickup.Count - 1);

                if (shipfuelPickup.Count == 0)
                    canUseShipFuel = false;
            }
        }
        //-------------------------Farhan's Code-------------------------
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
                itemToBeAdded.gameObject.SetActive(false);      //Should be destroyed but causes issues with reward system.
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

    //-------------------------Farhan's Code-------------------------
    public void SortItems(Item pickup)
    {
        if (pickup.itemID == 1)
        {
            for (int i = 0; i < pickup.amountInStack; i++)
            {
                healthPickup.Add(pickup);

                if (healthPickup.Count >= 1)
                    canUseHealth = true;

                else if (healthPickup.Count == 0)
                    canUseHealth = false;
            }
        }

        //------------------------------Requires Additional Tasks To Be Completed-------------------------
        if (pickup.itemID == 2)
        {
            for (int i = 0; i < pickup.amountInStack; i++)
            {
                shipfuelPickup.Add(pickup);

                if (shipfuelPickup.Count >= 1)
                    canUseShipFuel = true;

                else if (shipfuelPickup.Count == 0)
                    canUseShipFuel = false;
            }
        }
        //------------------------------Requires Additional Tasks To Be Completed-------------------------
    }

    public void UpdateScore()
    {
        scoreText.text = "Coins: " + scoreCount;
        if (highScore < scoreCount)
        {
            highScore = scoreCount;
            highscoreText.text = "Highscore: " + highScore;
        }
    }

    public void EndGame()
    {
        Debug.Log("You Won");
        winScreen.SetActive(true);
        finalHighScore.text = "Highscore: " + highScore;
        inventoryObject.SetActive(true);
        UpdateScore();
        SaveSystem.SavePlayer(healthSystem, this);
    }
    //-------------------------Farhan's Code-------------------------

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Item>())
        {
            AddItem(col.GetComponent<Item>());

            //Farhan's Code
            SortItems(col.GetComponent<Item>());
        }

        //-------------------------Farhan's Code-------------------------
        if (col.gameObject.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            scoreCount++;
            UpdateScore();
        }
        //-------------------------Farhan's Code-------------------------

        else if (col.CompareTag("Gem"))
        {
            gems.value++;
            EndGame();
        }
    }
}
