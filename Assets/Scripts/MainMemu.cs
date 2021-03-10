using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMemu : MonoBehaviour
{
    public string newGameScene;
    public GameObject continueButton;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.HasSave())
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }

    public void Continue() 
    {
        anim.SetTrigger("FadeMenu");
    }

    public void NewGame()
    {
        SaveSystem.NewGame();
        anim.SetTrigger("FadeMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
