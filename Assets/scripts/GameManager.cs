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
        instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefsX.SetStringArray("itemsHeld", itemsHeld);
            PlayerPrefsX.SetIntArray("itemCounts", numberOfItems);
            print(itemsHeld[i]);
        }
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("itemsHeld") && PlayerPrefs.HasKey("itemCounts"))
        {
            itemsHeld = PlayerPrefsX.GetStringArray("itemsHeld");
            numberOfItems = PlayerPrefsX.GetIntArray("itemCounts");
        }
    }
}
