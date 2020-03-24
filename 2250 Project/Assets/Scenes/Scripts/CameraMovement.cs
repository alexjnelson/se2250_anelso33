using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target; 
    public GameObject player;

    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player")==null){
            Instantiate(player, new Vector3(0,0,0), Quaternion.identity);
        }
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
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
