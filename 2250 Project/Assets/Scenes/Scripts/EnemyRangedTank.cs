using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedTank : EnemyRanged
{
    public Item GoldenSword, GoldenLauncher;

    override protected void GenerateDroppedItem(){
        droppedItem = PlayerMovement.instance.outfit == 0 ? GoldenSword : GoldenLauncher;
    }
}
