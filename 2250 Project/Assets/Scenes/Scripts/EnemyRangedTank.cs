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
            GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
        }
    }
}
