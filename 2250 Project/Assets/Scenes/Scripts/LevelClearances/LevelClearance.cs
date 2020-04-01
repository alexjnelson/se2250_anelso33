using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearance : MonoBehaviour
{
    protected int enemiesSpawned, enemiesToSpawn, levelNumber;

    virtual protected void Update()
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

    virtual protected void SpawnEnemies(){

    }
}
