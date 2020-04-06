using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedTank : EnemyRanged
{
    public Item GoldenSword, GoldenLauncher;
    private string _storyText = "It's mom! You saved her! And such ends our noble quest..."; // as the final boss, this enemy produces a storytext

    // the final dropped item is a golden weapon, tailored for the player's combat type
    override protected void GenerateDroppedItem(){
        droppedItem = PlayerMovement.instance.outfit == 0 ? GoldenSword : GoldenLauncher;
    }

    // on its death, it runs regular base enemy death functions (ie drop item, exp) but then initiates a story blurb. Also destroys any 
    // straggling enemies left in the level
    override protected void OnDestroy(){
        base.OnDestroy();
        if (GetComponent<Health>().health<=0){
            GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy")){
                Destroy(obj);
            }
        }
    }
}
