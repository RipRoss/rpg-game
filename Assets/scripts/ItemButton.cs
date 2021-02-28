using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int itemAmount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        if (GameManager.instance.itemsHeld[itemAmount] != "") {
            Inventory.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[itemAmount]));
        }
    }
}
