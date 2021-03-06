using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMemu : MonoBehaviour
{
    public string newGameScene;
    public GameObject continueButton;
    public Animator anim;

    public bool continueEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        if (continueEnabled)
        {
            continueButton.SetActive(true);
        } else
        {
            continueButton.SetActive(false);
        }
    }

    public void Continue() 
    {

    }

    public void NewGame()
    {
        anim.SetTrigger("FadeMenu");   
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
