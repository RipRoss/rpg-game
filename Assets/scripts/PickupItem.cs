using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item[] pickups;
    public bool[] pickupsToShow;
    public static PickupItem instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        LoadPickUpData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SavePickupData();
        } 

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadPickUpData();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GetComponent<Item>().isGold)
            {
                GameManager.instance.addMoney(GetComponent<Item>().amountToChange);
            } else
            {
                GameManager.instance.addItem(GetComponent<Item>().itemName);
            }

            GetComponent<Item>().gameObject.SetActive(false); // this may cause issues with multiple items.. but should be good
        }
    }

    public void SavePickupData()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i].gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName + "_" + i, 1);
                pickupsToShow[i] = true;
            } else {
                pickupsToShow[i] = false;
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName + "_" + i, 0);
            }
        }
    }

    public void LoadPickUpData()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            int intValueToSet = 0;

            if (PlayerPrefs.HasKey("StartScene_" + pickups[i].itemName + "_" + i))
            {
                intValueToSet = PlayerPrefs.GetInt("StartScene_" + pickups[i].itemName + "_" + i);
            } else
            {
                intValueToSet = 1;
            }

            if (intValueToSet == 0)
            {
                pickups[i].gameObject.SetActive(false);
                pickupsToShow[i] = false;
            } else
            {
                pickups[i].gameObject.SetActive(true);
                pickupsToShow[i] = true;
            }
        }
    }

    public void SetAllTrue()
    {
        for (int i = 0; i < pickups.Length; i++)
        {

            if (PlayerPrefs.HasKey("StartScene_" + pickups[i].itemName + "_" + i))
            {
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName + "_" + i, 1);
            }
        }
        SavePickupData();
        LoadPickUpData();
    }
}
