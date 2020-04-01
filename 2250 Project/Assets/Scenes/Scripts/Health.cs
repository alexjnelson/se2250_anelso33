using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //can be used for many entities
    public float health = 100;
    public HealthBar healthBar;
    void Start()

    {
        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float dmg)
    {
        this.health -= dmg;
        healthBar.SetHealth(this.health);
    }

}