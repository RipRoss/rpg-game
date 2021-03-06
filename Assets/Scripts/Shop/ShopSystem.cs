﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public string[] itemsToSell;
    public ShopButton[] shopSlots;
    public int[] stockAmount;
    public string shopName;
    public static Item selectedItem;

    public static ShopSystem instance;

    private void Start()
    {
        instance = this;

        ShowItemsInShop();        
    }

    public void ShowItemsInShop()
    {
        // loop over the items above
        // set the itemSlot variable in ShopButton to the i value
        // set the image and text values of the button
        // the stockText is stockAmount[i] value
        // set it to active (have them inactive by default)

        for (int i = 0; i < itemsToSell.Length; i++)
        {
            Transform shopButton = shopSlots[i].gameObject.transform;
            Transform itemImage = shopButton.Find("ItemImage");
            Transform textTrans = shopButton.Find("Text");

            if (stockAmount[i] <= 0)
            {
                itemImage.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
            }

            shopSlots[i].itemSlot = i;
            textTrans.gameObject.GetComponent<Text>().text = stockAmount[i].ToString();
            shopButton.gameObject.SetActive(true);
            itemImage.GetComponent<Image>().sprite = GameManager.instance.GetItemDetails(itemsToSell[i]).itemSprite;
        }
    }

    public void BuyItem()
    {
        int itemIndex = Array.IndexOf(itemsToSell, selectedItem.itemName);

        if (stockAmount[itemIndex] == 0)
        {
            // if the stock is empty, do nothing - we will display something at a later date
            Debug.LogError("WE ARE SOLD OUT OF : " + selectedItem.itemName);
            return;
        }

        if (selectedItem.sellValue <= GameManager.instance.currentGold)
        {
            // we need to remove stock here
            GameManager.instance.addItem(selectedItem);
            stockAmount[itemIndex]--;
            GameManager.instance.removeMoney(selectedItem.buyValue);
            ShowItemsInShop();
        } else
        {
            Debug.LogError("YOU CAN'T AFFORD THAT ITEM, YOU ONLY HAVE " + GameManager.instance.currentGold + " GOLD LEFT");
        }
    }

    public void SellItem()
    {
        // In reality, when we have the UI, the 'Sell' button will be greyed out, if you do not have the item.

        int itemIndex = Array.IndexOf(itemsToSell, selectedItem.itemName);

        if (!GameManager.instance.itemsHeld.Contains(selectedItem.itemName)) // for now we will check here if the user has the item, and tell the person they do not have that item
        {
            // if the stock is empty, do nothing - we will display something at a later date
            Debug.LogError("YOU DO NOT HAVE THIS ITEM : " + selectedItem.itemName);
            return;
        }

        // we need to remove stock here
        GameManager.instance.RemoveItem(selectedItem.itemName, 1); // the amount to remove, will be the amount put into the UI... but  for now thiss will do
        stockAmount[itemIndex]++; // this will be += the amount above
        GameManager.instance.addMoney(selectedItem.sellValue);
        ShowItemsInShop();
    }

    public void SelectItem(Item itemToSelect) 
    {
        selectedItem = itemToSelect;
    }
}
