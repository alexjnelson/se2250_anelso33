using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleItem : Item
{
    public static string [] attackTypes = {"stab", "slice", "throw"};
    public float damage, rangeX, rangeZ, attackSpeed; // attack speed is how many times the attack can be used per second
    public Sprite active, inactive;

    // the default battle item is usually assigned to enemies (melee and ranged, but not tank) and has the following stats
    public BattleItem(){
        damage = 15f;
        rangeX = 0.4f;
        rangeZ = 0.4f;
        attackSpeed = 0.8f;
    }

    // when a battle item is selected in the inventory, it first checks if it is already the selected item. If it is, it deselects itself
    // and the default item is used. If not, it replaces the currently selected battle item
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
