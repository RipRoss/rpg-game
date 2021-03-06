using System;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    /* TODO : 
        We have to add a collider to the items to ensure they don't end up places the player can't get as there's an element of gravity involved.
     
     */

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private SpriteRenderer sRenderer;
    public static ItemButton itemSlotDraggedFrom;
    public static Item replicatedItem;
    public static ItemButton itemMovingTo;
    private static bool ctrlPressed = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            ctrlPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ctrlPressed = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
            int amountToDrop = GameManager.instance.numberOfItems[eventData.pointerDrag.GetComponent<ItemButton>().itemAmount];

            if (ctrlPressed)
            {
                GameObject go = GameObject.Find("DropUI");
                GameObject canvas = go.transform.Find("DropCanvas").gameObject;
                DropPanel.item = replicatedItem;
                DropPanel.wasDragged = true;
                DropPanel.wasDropped = true;
                canvas.SetActive(true);
            } else
            {
                GameManager.instance.DropItem(replicatedItem, true, amountToDrop);
            }

            Destroy(replicatedItem.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Destroy(replicatedItem.gameObject);

        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerEnter.transform.parent.name != "ItemButtons")
            {
                itemMovingTo = eventData.pointerEnter.transform.parent.GetComponent<ItemButton>();
                MoveItemInInventory(itemMovingTo);
            } else
            {
                itemMovingTo = eventData.pointerEnter.GetComponent<ItemButton>();
                MoveItemInInventory(eventData.pointerEnter.GetComponent<ItemButton>());
            }            
        }
    }

    public static void MoveItemInInventory(ItemButton movedtoItemSlot, bool selectAmount = false, int amountToMove = 0)
    {
        GameManager gm = GameManager.instance;

        if (ctrlPressed)
        {
            // show the UI for how many you want to switch over
            GameObject go = GameObject.Find("DropUI");
            GameObject canvas = go.transform.Find("DropCanvas").gameObject;
            DropPanel.item = replicatedItem;
            canvas.SetActive(true);
            Destroy(replicatedItem.gameObject);
            return;
        }

        if (gm.itemsHeld[movedtoItemSlot.itemAmount] != "")
        {
            if (gm.itemsHeld[movedtoItemSlot.itemAmount] == replicatedItem.itemName)
            {
                int amount = Int32.Parse(itemSlotDraggedFrom.amountText.text);

                if (!selectAmount)
                {
                    gm.numberOfItems[itemSlotDraggedFrom.itemAmount] = 0;
                    gm.itemsHeld[itemSlotDraggedFrom.itemAmount] = "";
                } else
                {
                    amount = amountToMove;
                }

                gm.numberOfItems[movedtoItemSlot.itemAmount] += amount;
            }
            else
            {
                // currently, it won't let you split a stack into a stack of something that isn't of the same type - everything will get flipped.

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
            int amount = Int32.Parse(itemSlotDraggedFrom.GetComponent<ItemButton>().amountText.text);

            if (!selectAmount)
            {
                gm.numberOfItems[itemSlotDraggedFrom.itemAmount] = 0;
                gm.itemsHeld[itemSlotDraggedFrom.itemAmount] = "";
            }
            else
            {
                gm.numberOfItems[itemSlotDraggedFrom.itemAmount] -= amountToMove;
                amount = amountToMove;
            }

            // just add a new one
            gm.itemsHeld[movedtoItemSlot.itemAmount] = replicatedItem.itemName;
            gm.numberOfItems[movedtoItemSlot.itemAmount] = amount;
        }

        Inventory.instance.showItems();
    }
}
