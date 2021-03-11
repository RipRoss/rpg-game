using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    void OnApplicationQuit()
    {
        SaveData();
        PickupItem.instance.SavePickupData();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InvokeRepeating("SaveData", 0.0f, 10.0f);

        LoadData();
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    private int GetAmountInInventory(string itemName)
    {
        int amount = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemName)
            {
                // this is the item we want to return the count for.
                amount += numberOfItems[i];
            }
        }

        return amount;
    }

    public void addItem(Item item) 
    {
        int newItemPosition = 0;
        bool foundSpace = false;
        bool foundItem = itemsHeld.Contains(item.itemName);

        if (item.isGold)
        {
            addMoney(item.amountToChange);
        }

        // this finds first available space, even if t here's 
        if (!foundItem)
        {
            for (int i = 0; i < itemsHeld.Length; i++)
            {
                if (itemsHeld[i] == "")
                {
                    newItemPosition = i;
                    foundSpace = true;
                    break;
                }
            }
        } else
        {
            newItemPosition = Array.IndexOf(itemsHeld, item.itemName);
        }
        

        if (foundSpace || foundItem)
        {
            for (int i = 0; i < referenceItems.Length; i++) { 
                if (referenceItems[i].itemName == item.itemName)
                {
                    itemsHeld[newItemPosition] = item.itemName;
                    numberOfItems[newItemPosition]++;
                    break;
                }
            }
        }

        Inventory.instance.showItems();
    }

    public void RemoveItem(string itemName, int amountToRemove)
    {
        /*
         Loop over the items held
         if the item we are trying to remove, equals the item that we are trying to get
         either set the value to 0, if it's all of them
         - amountToRemove if the value of that is not 0.
         */


        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemName)
            {
                if (amountToRemove == 0)
                {
                    itemsHeld[i] = "";
                    numberOfItems[i] = 0;
                }
                else
                {
                    numberOfItems[i] -= amountToRemove;

                    if (numberOfItems[i] <= 0)
                    {
                        itemsHeld[i] = "";
                    }
                    break;
                }
            }
        }

        Inventory.instance.showItems();
    }

    public void DropItem(Item item, bool dragDrop, int amountToDrop = 0)
    {
        Item[] pUps = Items.instance.itemList;
        Pickup.canPickUp = false;
        for (int i = 0; i < pUps.Length; i ++)
        {
            if (pUps[i].itemName == item.itemName)
            {
                Vector3 direction;
                Vector3 dropPos = new Vector3(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y, PlayerController.instance.transform.position.z);
                float multiply;

                if (dragDrop)
                {
                    Vector3 mousePos = Input.mousePosition;
                    Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos);
                    direction = dir - dropPos;
                    multiply = .6f;
                } else
                {
                    direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
                    multiply = 2f;
                }

                if (amountToDrop == 0)
                {
                    // do all of them
                    for (int x = 0; x < GetAmountInInventory(item.itemName); x++)
                    {
                        GameObject go = Instantiate(pUps[i].gameObject);
                        go.SetActive(true);
                        go.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                        go.transform.position = dropPos;
                        go.GetComponent<Rigidbody2D>().AddForce(direction * multiply, ForceMode2D.Impulse);
                    }
                } else
                {
                    // loop over amountToDrop here
                    for (int x = 0; x < amountToDrop; x++)
                    {
                        GameObject go = Instantiate(pUps[i].gameObject);
                        go.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
                        go.SetActive(true);
                        go.transform.position = dropPos;
                        go.GetComponent<Rigidbody2D>().AddForce(direction * multiply, ForceMode2D.Impulse);
                    }
                }
                RemoveItem(item.itemName, amountToDrop);
                Invoke("SetCanPickUp", .5f);
            }        
        }
    }

    private void SetCanPickUp()
    {
        Pickup.canPickUp = true;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }

        Inventory.instance.showItems();
    }

    public void addMoney(int amount)
    {
        currentGold += amount;
        Inventory.instance.goldText.text = currentGold + "g";
    }

    public void removeMoney(int amount)
    {
        currentGold -= amount;
        Inventory.instance.goldText.text = currentGold + "g";
    }

    public void SaveData()
    {
        SaveSystem.SavePlayer();
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            currentGold = data.gold;
            itemsHeld = data.itemsHeld;
            numberOfItems = data.numberOfItems;
            PlayerController.instance.sceneName = data.sceneName;

            if (!PlayerController.instance.spawned)
            {
                SceneManager.LoadScene(data.sceneName); // i think we need to do this at the main menu stage
                PlayerController.instance.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                PlayerController.instance.spawned = true;
                XPManager.instance.playerLevel = data.playerLevel;
                XPManager.instance.currentXP = data.currentXP;
                XPManager.instance.targetXP = data.targetXP;
            }
        }
    }
}
