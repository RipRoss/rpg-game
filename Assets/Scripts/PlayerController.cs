using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float panSpeed;
    public static PlayerController instance;
    public string areaTransitionName;
    public string sceneName;
    public bool canMove = true;
    public bool spawned = false;

    public int currentHealth;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // dont destroy the player
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos.y += panSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.A))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                pos.y -= panSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                pos.x += panSpeed * Time.deltaTime;
            }
        }        

        transform.position = pos;
    }

}
