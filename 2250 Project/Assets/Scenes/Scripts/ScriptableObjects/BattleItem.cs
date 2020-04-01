﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleItem : Item
{
    public static string [] attackTypes = {"stab", "slice", "throw"};
    public float damage, rangeX, rangeZ, attackSpeed; // attack speed is how many times the attack can be used per second
    public Sprite active, inactive;

    public BattleItem(){
        damage = 15f;
        rangeX = 0.4f;
        rangeZ = 0.4f;
        attackSpeed = 0.8f;
    }

    public override void Use(){
        BattleItemScript script = PlayerMovement.instance.gameObject.GetComponent<BattleItemScript>();
        if (script.item == this){ 
            script.removeItem(); 
            this.itemSprite = inactive;
        }
        else { 
            script.setItem(this); 
            this.itemSprite = active;
        }
    }
}
