using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float panSpeed;
    public static PlayerController instance;
    public string areaTransitionName;
    public bool canMove = true;
    public bool spawned = false;

    public int maxHealth = 10;
    public int currentHealth;
    public PlayerHealth healthBar;

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
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) //Fake damage
        {
            TakeDamage(1);
        }

        if(Input.GetKeyDown(KeyCode.E)) //Fake heal
        {
            HealPlayer(1);
        }


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


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealth();
    }

    void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthBar.SetHealth(currentHealth);
    }


}
