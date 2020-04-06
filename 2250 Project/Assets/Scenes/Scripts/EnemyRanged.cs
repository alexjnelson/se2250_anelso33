using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    // overrides base class CheckAttack; uses ranged attack instead of regular attack
    protected override bool CheckAttack(Vector3 playerPosition) {
        if (Vector3.Distance(playerPosition, currentPosition) <  attackRange && GameObject.FindWithTag("Player") != null){
            GetComponent<Combat>().AttackRanged();
            return true;
        }
        else { return false; }
    }
}
