using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    protected override bool CheckAttack(Vector3 playerPosition) {
        if (Vector3.Distance(playerPosition, currentPosition) <  3 && GameObject.FindWithTag("Player") != null){
            GetComponent<Combat>().AttackRanged();
            return true;
        }
        else { return false; }
    }
}
