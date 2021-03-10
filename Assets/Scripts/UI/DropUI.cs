using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUI : MonoBehaviour
{
    private static DropUI instance;
    // Start is called before the first frame update
    void Start()
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
}
