using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //can be used for many entities
    public float health = 100f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }        
    }

    public void Damage(int dmg){
        this.health-=dmg;
    }

}