using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public string areaToLoad = "";
    public string areaTransitionName;
    public Animator anim;
    public AreaEntrance theEntrance;

    void Start() {
        //theEntrance.transitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name != "NewScene")
            {
                PickupItem.instance.SavePickupData();
            }

            // GameManager.instance.SaveData();

            anim.SetTrigger("Fade");
            PlayerController.instance.canMove = false;
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

    public void OnAnimationComplete()
    {
        SceneManager.LoadScene(areaToLoad);
    }
}
