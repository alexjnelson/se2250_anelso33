using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Transform target;
    public string nextScene;

    // when the player enters a transfer zone between scenes, they are moved to the next scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")||other.CompareTag("Mom")) && PlayerMovement.instance.allowExit == true)
        {
            StartCoroutine(LoadAsyncScene(other.gameObject));           
        }
    }

    // two scenes must be simulatneously loaded so the player can be moved to the scene. After this transfer, the original scene is unloaded
     IEnumerator LoadAsyncScene(GameObject player)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(nextScene));
        SceneManager.UnloadSceneAsync(currentScene);
        player.transform.position = target.position; // the player is spawned at the transfer location in the next scene
    }
}
