using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerDownHandler 
{
    public Image buttonImage;
    public Text amountText;
    public int itemAmount;

    public void Press()
    {
        if (GameManager.instance.itemsHeld[itemAmount] != "") {
            Inventory.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[itemAmount]));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown : " + GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[itemAmount]));
    }
}
