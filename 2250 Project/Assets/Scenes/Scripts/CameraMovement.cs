using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target; 
    public GameObject player;
    public static GameObject playerSave;

    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;


    // this method loads the appropriate player script; if a save is requested, it loads that; otherwise it loads a new player. It then sets the
    // camera's focus to be this player.
    void Start()
    {
        if (GameObject.FindWithTag("Player")==null && playerSave == null){
            Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
        }
        else if (GameObject.FindWithTag("Player")==null){
            Instantiate(playerSave, new Vector3(0,0,0), Quaternion.identity);
        }
        target = GameObject.FindWithTag("Player").transform;
    }

    // late update moves the camera to follow the player's position
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 playerPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            playerPosition.x = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
            playerPosition.y = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);
        }
    }
}
