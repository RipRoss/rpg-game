using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        print(transitionName);
        print(PlayerController.instance.areaTransitionName);

        if(transitionName == PlayerController.instance.areaTransitionName)
        {
            PlayerController.instance.transform.position = this.transform.position;
        }
    }
}
