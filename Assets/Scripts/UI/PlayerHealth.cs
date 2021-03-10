using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Image HealthFill;
    public int MaxHealth = 10;
    public int CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthFill = GetComponent<Image>();

        CurrentHealth = MaxHealth;
        InvokeRepeating("PlayerDamage", 0, 2f);
    }

    void PlayerDamage()
    {
        CurrentHealth -= 1;
        int health = CurrentHealth / MaxHealth;
        SetHealth(health);
    }

    void SetHealth(int MyHealth)
    {
        HealthFill.fillAmount = MyHealth;
    }

}
