using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    protected int enemiesSpawned = 0, enemiesToSpawn = 8, levelNumber = 2;
    public GameObject enemyMelee, enemyRanged, enemyTank;
    public Transform target1, target2, target3, target4, target5, target6, target7, target8;
    private string _storyText ="'These fiends are everywhere... I bet they came from the spooky Northwest Forest!'";

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
        Instantiate(enemyMelee, target1).transform.position = target1.position;
        Instantiate(enemyMelee, target2).transform.position = target2.position;
        Instantiate(enemyMelee, target3).transform.position = target3.position;
        Instantiate(enemyRanged, target4).transform.position = target4.position;
        Instantiate(enemyRanged, target5).transform.position = target5.position;
        Instantiate(enemyMelee, target6).transform.position = target6.position;
        Instantiate(enemyMelee, target7).transform.position = target7.position;
        Instantiate(enemyTank, target8).transform.position = target8.position;
    }
}
