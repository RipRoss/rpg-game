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
            removeItem("Iron Armour");
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

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemName)
            {
                newItemPosition = i;
                foundSpace = true;
                break;
            }
        }

        if (foundSpace)
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

    public void removeItem(string itemName)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemName)
            {
                numberOfItems[i]--;
                break;
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
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            currentGold = data.gold;
            itemsHeld = data.itemsHeld;
            numberOfItems = data.numberOfItems;
            PlayerController.instance.areaTransitionName = data.areaTransitionName;
            print("data.position " + data.position[0]);
            print("data.position " + data.position[1]);
            print("data.position " + data.position[2]);

            if (!PlayerController.instance.spawned)
            {
                SceneManager.LoadScene(PlayerController.instance.areaTransitionName);
                PlayerController.instance.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                PlayerController.instance.spawned = true;
            }
        }
    }
}
