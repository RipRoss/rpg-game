using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float panSpeed;
    public static PlayerController instance;
    public string areaTransitionName;
    public bool canMove = true;

    [SerializeField] private Stat health;

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

        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));

        health.Initialize(100, 100);

    }


    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;

        //Health Debug
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.MyCurrentValue -= 1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            health.MyCurrentValue += 1;
        }

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
