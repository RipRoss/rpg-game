using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static bool canPickUp = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (canPickUp)
            {
                GameManager.instance.addItem(GetComponent<Item>());
                GetComponent<Item>().gameObject.SetActive(false);
            }            
        }
    }
}
