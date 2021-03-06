using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropPanel : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    public static Item item;
    public static bool wasDragged;
    public static bool wasDropped;

    public void Drop()
    {
        // TODO : check if there are only numbers in there. Check if there is enough in the inventory to drop them, or just drop them all? I think drop them all.
        if (wasDropped)
        {
            GameManager.instance.DropItem(item, false, Int32.Parse(inputField.text));
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            DragDrop.MoveItemInInventory(DragDrop.itemMovingTo, true, Int32.Parse(inputField.text));
        }        
    }
}
