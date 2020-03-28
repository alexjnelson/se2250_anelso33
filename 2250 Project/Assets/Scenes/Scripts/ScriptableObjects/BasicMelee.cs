using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : BattleItem
{
    public BasicMelee(){
        damage = 30f;
        rangeX = 2f;
        rangeZ = 2f;
        attackSpeed = 1.2f;
    }
}
