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
            GameManager.instance.addItem(GetComponent<Item>().itemName);

            GetComponent<Item>().gameObject.SetActive(false);
            print(GetComponent<Item>().gameObject.name);
        }
    }

    public void SavePickupData()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i].gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName, 1);
                pickupsToShow[i] = true;
            } else {
                pickupsToShow[i] = false;
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName, 0);
            }
        }
    }

    public void LoadPickUpData()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            int intValueToSet = 0;

            if (PlayerPrefs.HasKey("StartScene_" + pickups[i].itemName))
            {
                intValueToSet = PlayerPrefs.GetInt("StartScene_" + pickups[i].itemName);
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

            if (PlayerPrefs.HasKey("StartScene_" + pickups[i].itemName))
            {
                PlayerPrefs.SetInt("StartScene_" + pickups[i].itemName, 1);
            }
        }
        SavePickupData();
        LoadPickUpData();
    }
}
