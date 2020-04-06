using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //can be used for many entities
    public float health = 100f;

    // Constantly checks if the gameobject has been killed. If so, destroys it
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // called in Combat to deal damage to an entity
    public void Damage(float dmg){
        this.health-=dmg;
    }

}