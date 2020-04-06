using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    protected int enemiesSpawned = 0, enemiesToSpawn = 10, levelNumber = 3;
    public GameObject enemyMelee, enemyRanged, enemyGuard, enemyRangedTank;
    public Transform melee1, melee2, ranged1, melee3, melee4, ranged2, ranged3, guard1, guard2, rangedtank;
    private string _storyText ="Mom's voice resonates from deep within the forest...";

    void Update()
    {
        if (GameObject.FindWithTag("Player") != null){
            if (PlayerMovement.instance.levelsCleared<levelNumber && enemiesSpawned == 0){
                SpawnEnemies();
                enemiesSpawned = enemiesToSpawn;
                PlayerMovement.instance.allowExit = false;
                GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
            }
            else if (PlayerMovement.instance.levelsCleared<levelNumber && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                PlayerMovement.instance.levelsCleared = levelNumber;
                PlayerMovement.instance.allowExit = true;
            }
        }
    }

    void SpawnEnemies(){
        Instantiate(enemyMelee, melee1).transform.position = melee1.position;
        Instantiate(enemyMelee, melee2).transform.position = melee2.position;
        Instantiate(enemyRanged, ranged1).transform.position = ranged1.position;
        Instantiate(enemyMelee, melee3).transform.position = melee3.position;
        Instantiate(enemyMelee, melee4).transform.position = melee4.position;
        Instantiate(enemyRanged, ranged2).transform.position = ranged2.position;
        Instantiate(enemyRanged, ranged3).transform.position = ranged3.position;
        Instantiate(enemyGuard, guard1).transform.position = guard1.position;
        Instantiate(enemyGuard, guard2).transform.position = guard2.position;
        Instantiate(enemyRangedTank, rangedtank).transform.position = rangedtank.position;
    }
}
