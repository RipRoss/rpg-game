using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public int mana;
    public int energy;
    public int gold = 0; 
    public int[] numberOfItems;

    public string[] itemsHeld;
    public float[] position;

    public string areaTransitionName;

    public PlayerData ()
    {
        gold = GameManager.instance.currentGold;
        level = 1;
        health = 100;
        mana = 100;
        energy = 100;
        areaTransitionName = SceneManager.GetActiveScene().name;
        itemsHeld = GameManager.instance.itemsHeld;
        numberOfItems = GameManager.instance.numberOfItems;

        position = new float[3];
        position[0] = PlayerController.instance.transform.position.x;
        position[1] = PlayerController.instance.transform.position.y;
        position[2] = PlayerController.instance.transform.position.z;
    }
}
