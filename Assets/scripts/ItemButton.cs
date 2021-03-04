using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
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
}
