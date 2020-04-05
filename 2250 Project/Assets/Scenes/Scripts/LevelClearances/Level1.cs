using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    protected int enemiesSpawned = 0, enemiesToSpawn = 2, levelNumber = 1;
    public GameObject enemy;
    public Transform target1, target2;
    private string _storyText ="'Who are these hostiles in my house? They look like trees... What did they do with my mother?'\nYou burst into an uncontrollable rage.";

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
        Instantiate(enemy, target1).transform.position = target1.position;
        Instantiate(enemy, target2).transform.position = target2.position;
    }
}
