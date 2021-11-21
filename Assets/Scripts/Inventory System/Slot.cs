using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    //public Inventory inventory;
    public Item slotsItem;
    [SerializeField] GameObject player;
    Sprite defaultSprite;
    Text amountText;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void CustomStart()
    {
        defaultSprite = GetComponent<Image>().sprite;
        amountText = transform.GetChild(0).GetComponent<Text>();
        amountText.text = "";
    }
    public void DropItem()
    {
        Vector3 playerPos = player.transform.position;

        if (slotsItem)
        {
            slotsItem.transform.parent = null;
            slotsItem.gameObject.SetActive(true);
            slotsItem.transform.position = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z + 2);
        }
    }
    public void CheckForItem()
    {
        if (transform.childCount > 1)
        {
            slotsItem = transform.GetChild(1).GetComponent<Item>();
            GetComponent<Image>().sprite = slotsItem.itemSprite;
            if (slotsItem.amountInStack > 1)
                amountText.text = slotsItem.amountInStack.ToString();
        }
        else
        {
            slotsItem = null;
            GetComponent<Image>().sprite = defaultSprite;
            amountText.text = "";
        }
    }
}