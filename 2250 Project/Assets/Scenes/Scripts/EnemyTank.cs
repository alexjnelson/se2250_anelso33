using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{
    public Item LongSword, SpringLauncher;
    // as the level boss, drops a special battle item tailored to the player's combat style
    override protected void GenerateDroppedItem(){
        droppedItem = PlayerMovement.instance.outfit == 0 ? LongSword : SpringLauncher;
    }
}
