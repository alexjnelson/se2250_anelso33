using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potion : Item
{
    public float health;

    public void Use(){
        GameObject.FindWithTag("Player").GetComponent<Health>().Damage(-health);
    }
}
