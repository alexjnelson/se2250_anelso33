using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potion : Item
{
    public float health; // health restored differs by potion size

    // a potion restores its specified health and consumes itself when used. If the potion would restore health above 100, it is not consumed. 
    public override void Use(){
        PlayerMovement player = PlayerMovement.instance;
        if (player.gameObject.GetComponent<Health>().health + health <= 100){
            player.gameObject.GetComponent<Health>().Damage(-health);
            player.gameObject.GetComponent<Bag>().removeItem(this);
        }
    }
}
