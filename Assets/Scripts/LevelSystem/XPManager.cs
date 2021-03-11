using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    // theser values need to be saved as part of the XP manager. Can we make something a little more generic? Maybe... we'll see
    public Text textForXP;
    public int currentXP, targetXP, playerLevel; // currentXP is the xp the player is at, the targetXP is the XP to next 

    public static XPManager instance;

    private void Start()
    {
        if (instance == null) 
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            IncreaseXP(50);
        }
    }

    public void IncreaseXP(int xpAmount)
    {
        currentXP += xpAmount;
        if (currentXP >= targetXP)
        {
            SetTargetXP();
            LevelPlayerUp();
            print("player has levelled up");
        }
    }

    private void SetTargetXP()
    {
        // the formular is 2.43 * the previous current XP
        targetXP = (int)(targetXP * 1.7); // int coverts to float, works out the calculation - and casts it back to an integer. we are making the current value of targetXP... the value of targetXP * 1.43. ie 100 * 2.43 = targetXP of 243 for level 3. Yours is 100 something, so you need another 100 something to lvl
    }    

    private void LevelPlayerUp()
    {
        playerLevel++;
        // play some form of animation, from the player - to inform the player, that he/she has levelled - to be done
    }
}
