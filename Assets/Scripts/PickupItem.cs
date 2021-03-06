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


    // switch this over to use the new system as we no longer save things in 'PlayerPrefs'
    public void SavePickupData()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i].gameObject.activeSelf)
            {
                // active
                pickupsToShow[i] = true;
            } else
            {
                // not active
                pickupsToShow[i] = false;
            }
        }

        SaveSystem.SaveWorld();
    }

    public void LoadPickUpData()
    {
        WorldData data = SaveSystem.LoadWorld();

        if (data != null)
        {
            pickupsToShow = data.pickupsToShow;

            if (pickups.Length > 1)
            {
                for (int i = 0; i < pickupsToShow.Length; i++)
                {
                    if (pickupsToShow[i])
                    {
                        pickups[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        pickups[i].gameObject.SetActive(false);
                    }
                }
            }
        }        
    }

    public void SetAllTrue()
    {
        for (int i = 0; i < pickups.Length - 1; i++)
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
