using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isGold;
    public bool isItem;
    public bool isWeapon;
    public bool isArmour;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectMR, affectStr; // not used currently, placeholders for future

    [Header("Weapon/Armour Details")]
    public int weaponStr; // not used currently, placeholders for future
    public int armourStr; // not used currently, placeholders for future

    public void Use()
    {
        if (affectHP)
        {
            // we can do maxHealth - currrentHealth to work out the difference. Ie, we never want to go above 100 health. 
            // PlayerController.instance.currentHealth += 100;
        }
    }
}
