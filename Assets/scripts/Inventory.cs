using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription; // useButtonText; // this is for future when we switch between use and equip

    public GameObject inventory;
    public Text goldText;
    public bool open;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // dont destroy the player
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (open)
            {
                open = false;
                inventory.SetActive(false);
            }
            else 
            {
                open = true;
                showItems();
                inventory.SetActive(true);
            }
        }

    }

    public void showItems()
    {
        //GameManager.instance.SortItems();
        goldText.text = GameManager.instance.currentGold + "g";
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].itemAmount = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            } else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void DropItem()
    {
        GameManager.instance.DropItem(activeItem, false);
    }

    public void UseItem()
    {
        activeItem.Use();
        GameManager.instance.RemoveItem(activeItem.itemName);
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
    }
}
