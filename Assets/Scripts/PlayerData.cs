using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int health;
    public int mana;
    public int energy;
    public int gold = 0;
    public int[] numberOfItems;
    public string[] itemsHeld;
    public float[] position;
    public int currentXP;
    public int targetXP;
    public int playerLevel;

    public string sceneName;

    public PlayerData ()
    {
        gold = GameManager.instance.currentGold;
        playerLevel = XPManager.instance.playerLevel;
        currentXP = XPManager.instance.currentXP;
        targetXP = XPManager.instance.targetXP;
        health = 100;
        mana = 100;
        energy = 100;
        sceneName = SceneManager.GetActiveScene().name;
        itemsHeld = GameManager.instance.itemsHeld;
        numberOfItems = GameManager.instance.numberOfItems;

        Debug.Log(playerLevel);

        position = new float[3];
        position[0] = PlayerController.instance.transform.position.x;
        position[1] = PlayerController.instance.transform.position.y;
        position[2] = PlayerController.instance.transform.position.z;
    }
}
