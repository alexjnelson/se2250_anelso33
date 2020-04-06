using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedTank : EnemyRanged
{
    public Item GoldenSword, GoldenLauncher;
    private string _storyText = "Mom is trapped... She says they took the keys into the cave to the North.";

    override protected void GenerateDroppedItem(){
        droppedItem = PlayerMovement.instance.outfit == 0 ? GoldenSword : GoldenLauncher;
    }

    override protected void OnDestroy(){
        base.OnDestroy();
        if (GetComponent<Health>().health<=0){
            GameObject enemyClear = Instantiate(GetComponent<Combat>().AttackHitBox, new Vector3(0,0,0), Quaternion.identity);
            enemyClear.GetComponent<BoxCollider2D>().size = new Vector3(100, 100, 1);
            enemyClear.GetComponent<AttackCollider>().attackTime = 3;
            enemyClear.tag = "PlayerAttack";

            GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
        }
    }
}
