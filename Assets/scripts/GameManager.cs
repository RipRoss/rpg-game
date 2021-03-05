﻿using System;
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
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InvokeRepeating("SaveData", 0.0f, 10.0f);

        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            addItem("Iron Armour");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            RemoveItem("Iron Armour");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            removeMoney(100);
        }

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            SortItems();
        }*/
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

    public void addItem(string itemName) 
    {
        int newItemPosition = 0;
        bool foundSpace = false;
        bool foundItem = itemsHeld.Contains(itemName);


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
            newItemPosition = Array.IndexOf(itemsHeld, itemName);
        }
        

        if (foundSpace || foundItem)
        {
            for (int i = 0; i < referenceItems.Length; i++) { 
                if (referenceItems[i].itemName == itemName)
                {
                    itemsHeld[newItemPosition] = itemName;
                    numberOfItems[newItemPosition]++;
                    break;
                }
            }
        }

        Inventory.instance.showItems();
    }

    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemName)
            {
                numberOfItems[i]--;

                if (numberOfItems[i] <= 0)
                {
                    itemsHeld[i] = "";
                    Inventory.instance.activeItem = null;
                }
                break;
            }
        }

        Inventory.instance.showItems();
    }

    public void DropItem(Item item, bool dragDrop)
    {
        Item[] pUps = PickupItem.instance.pickups;
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
                
                GameObject go = Instantiate(pUps[i].gameObject);
                go.SetActive(true);
                go.transform.position = dropPos;
                go.GetComponent<Rigidbody2D>().AddForce(direction * multiply, ForceMode2D.Impulse);
                RemoveItem(item.itemName);
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
            }
        }
    }
}
