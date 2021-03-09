using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    // this class will be attached to the button that gets spawned, similar to the inventory. We need to create it exactly the same as the inventory.
    // Make slots without items invisible

    public Image buttonImage; // not sure if we need this, we will see how we do this when we have the shop UI
    public Text stockText; // this will display the amount available at the shop, at that time
    public int itemSlot; // when we create the grid, similar to how we created the inventory, this will be the index value which matches the amount and the item itself.

    public void Press() {
        print(this.gameObject.name);
        ShopSystem.instance.SelectItem(ShopSystem.instance.itemsToSell[itemSlot]);
        print(itemSlot);
    }
}
