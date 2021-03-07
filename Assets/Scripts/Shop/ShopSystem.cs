using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public Item[] itemsToSell;
    public int[] stockAmount;
    public string shopName;
    public Item selectedItem;

    private static ShopSystem instance;

    private void Start()
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

        //BuyItem();

        //ShowItemsInShop();        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    public void ShowItemsInShop()
    {
        for (int i = 0; i < itemsToSell.Length; i++)
        {
            GameManager.instance.addItem(itemsToSell[i]);
        }
    }

    public void BuyItem()
    {
        int itemIndex = Array.IndexOf(itemsToSell, selectedItem);
        print(stockAmount[itemIndex]);

        if (stockAmount[itemIndex] == 0)
        {
            // if the stock is empty, do nothing - we will display something at a later date
            Debug.LogError("WE ARE SOLD OUT OF : " + selectedItem.itemName);
            return;
        }

        print("Current Gold : " + GameManager.instance.currentGold);
        print("Item Cost : " + selectedItem.itemCost);

        if (selectedItem.itemCost <= GameManager.instance.currentGold)
        {
            // we need to remove stock here
            GameManager.instance.addItem(selectedItem);
            stockAmount[itemIndex]--;
            GameManager.instance.removeMoney(selectedItem.itemCost);

        } else
        {
            Debug.LogError("YOU CAN'T AFFORD THAT ITEM, YOU ONLY HAVE " + GameManager.instance.currentGold + " GOLD LEFT");
        }
    }

    public void SellItem()
    {

    }

    public void SelectItem() 
    {
        
    }
}
