using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleItem : Item
{
    public static string [] attackTypes = {"stab", "slice", "throw"};
    public float damage, rangeX, rangeZ, attackSpeed; // attack speed is how many times the attack can be used per second

    public BattleItem(){
        damage = 20f;
        rangeX = 2f;
        rangeZ = 2f;
        attackSpeed = 0.8f;
    }

    void Use(){
        PlayerMovement.instance.gameObject.GetComponent<BattleItemScript>().setItem(this);
    }
}
