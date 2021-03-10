using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    [SerializeField] private Text hintText;
    [SerializeField] private string message;
    [SerializeField] private ShopSystem shop;
    private bool shopOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!shopOpen) 
            {
                shop.ShowItemsInShop();
                shop.transform.Find("Panel").gameObject.SetActive(true);
                shopOpen = true;
                PlayerController.instance.moveEnabled = false;
                PlayerController.instance.anim.enabled = false;
            } else
            {
                shop.transform.Find("Panel").gameObject.SetActive(false);
                shopOpen = false;
                PlayerController.instance.moveEnabled = true;
                PlayerController.instance.anim.enabled = true;
            }
            
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {   
            hintText.text = message;
            hintText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            hintText.gameObject.SetActive(false);
        }
    }
}
