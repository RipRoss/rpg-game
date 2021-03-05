using System;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private SpriteRenderer sRenderer;
    private static ItemButton itemSlotDraggedFrom;
    private static Item replicatedItem;
    private static bool ctrlPressed = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ctrlPressed = true;
            print("helloworld");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        itemSlotDraggedFrom = eventData.pointerDrag.GetComponent<ItemButton>();

        if (itemSlotDraggedFrom.amountText.text != "")
        {
            replicatedItem = Instantiate(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[itemSlotDraggedFrom.itemAmount]));
            sRenderer = replicatedItem.GetComponent<SpriteRenderer>();
            sRenderer.color = new Color(1f, 1f, 1f, .6f); // set opacity lower
            // we co
            Vector3 mousePos = Input.mousePosition;
            Vector3 itemPos = Camera.main.ScreenToWorldPoint(mousePos);
            itemPos.z = 0;
            replicatedItem.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            replicatedItem.transform.position = itemPos;
            replicatedItem.gameObject.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 itemPos = Camera.main.ScreenToWorldPoint(mousePos);
        rectTransform = replicatedItem.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = itemPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // this is where the drop on the floor logic will go.

        if (eventData.pointerEnter == null)
        {
            GameManager.instance.DropItem(replicatedItem, true);     
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerEnter.transform.parent.name != "ItemButtons")
            {
                print("this is the parent " + eventData.pointerEnter.transform.parent.name);
                MoveItemInInventory(eventData.pointerEnter.transform.parent.GetComponent<ItemButton>());
            } else
            {
                print("this is not the parent " + eventData.pointerEnter.name);
                MoveItemInInventory(eventData.pointerEnter.GetComponent<ItemButton>());
            }            
        }
    }

    private void MoveItemInInventory(ItemButton movedtoItemSlot)
    {
        GameManager gm = GameManager.instance;

        // Get the ItemButton component for the item we have just dropped off at.
        // Use the itemAmount to add to itemsHeld and numberOfItems below

        if (gm.itemsHeld[movedtoItemSlot.itemAmount] != "")
        {
            if (gm.itemsHeld[movedtoItemSlot.itemAmount] == replicatedItem.itemName)
            {
                // loop over the count of items (amountText on the item.)
                // subtract for each one... or you can remove the 

                // add to the item, rather than replace the item
                gm.numberOfItems[movedtoItemSlot.itemAmount] = Int32.Parse(itemSlotDraggedFrom.amountText.text);

                gm.numberOfItems[itemSlotDraggedFrom.itemAmount] = 0;
                gm.itemsHeld[itemSlotDraggedFrom.itemAmount] = "";
            }
            else
            {
                string movedFromItemName = gm.itemsHeld[itemSlotDraggedFrom.itemAmount];
                string movedToItemName = gm.itemsHeld[movedtoItemSlot.itemAmount];
                int movedFromAmount = gm.numberOfItems[itemSlotDraggedFrom.itemAmount];
                int movedToAmount = gm.numberOfItems[movedtoItemSlot.itemAmount];

                // this is a different item, we want to switch the items around at this point
                gm.itemsHeld[itemSlotDraggedFrom.itemAmount] = movedToItemName;
                gm.numberOfItems[itemSlotDraggedFrom.itemAmount] = movedToAmount;
                gm.itemsHeld[movedtoItemSlot.itemAmount] = movedFromItemName;
                gm.numberOfItems[movedtoItemSlot.itemAmount] = movedFromAmount;
            }
        } else
        {
            // just add a new one
            gm.itemsHeld[movedtoItemSlot.itemAmount] = replicatedItem.itemName;
            print(Int32.Parse(itemSlotDraggedFrom.amountText.text));
            gm.numberOfItems[movedtoItemSlot.itemAmount] = Int32.Parse(itemSlotDraggedFrom.GetComponent<ItemButton>().amountText.text);

            // remove the items
            gm.itemsHeld[itemSlotDraggedFrom.itemAmount] = "";
            gm.numberOfItems[itemSlotDraggedFrom.itemAmount] = 0;
        }

        Inventory.instance.showItems();
    }
}
