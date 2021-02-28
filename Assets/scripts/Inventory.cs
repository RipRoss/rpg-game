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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
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
                //goldText.text = GameManager.instance.currentGold + "g";
                inventory.SetActive(true);
            }
        }

    }

    public void showItems() 
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].itemAmount = i;

            print(itemButtons[i].amountText);

            if (GameManager.instance.itemsHeld[i] != "" && GameManager.instance.numberOfItems[i] != 0)
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

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        /*if (activeItem.isItem)
        {
            // it's an item
        }

        if (activeItem.isWeapon || activeItem.isArmour)
        {
            // its a weapon
        }
    */
        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }
}
