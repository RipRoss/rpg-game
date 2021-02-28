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
        print("area    " + areaTransitionName);
        //theEntrance.transitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Fade");
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

    public void OnAnimationComplete()
    {
        SceneManager.LoadScene(areaToLoad);
    }
}
