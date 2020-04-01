using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potion : Item
{
    public float health;

    public override void Use(){
        PlayerMovement player = PlayerMovement.instance;
        player.gameObject.GetComponent<Health>().Damage(-health);
        player.playerBag.removeItem(this);
    }
}
