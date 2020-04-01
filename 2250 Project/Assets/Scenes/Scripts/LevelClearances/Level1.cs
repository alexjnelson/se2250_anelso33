using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : LevelClearance
{
    protected int enemiesSpawned = 0, enemiesToSpawn = 2, levelNumber = 1;
    public GameObject enemy;
    public Transform target1, target2;

    void Update()
    {
        if (GameObject.FindWithTag("Player") != null){
            if (PlayerMovement.instance.levelsCleared==levelNumber-1 && enemiesSpawned == 0){
                SpawnEnemies();
                enemiesSpawned = enemiesToSpawn;
                PlayerMovement.instance.allowExit = false;
            }
            else if (PlayerMovement.instance.levelsCleared==levelNumber-1 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                PlayerMovement.instance.levelsCleared = levelNumber;
                PlayerMovement.instance.allowExit = true;
            }
        }
    }

    void SpawnEnemies(){
        Instantiate(enemy, target1).transform.position = target1.position;
        Instantiate(enemy, target2).transform.position = target2.position;
    }
}
