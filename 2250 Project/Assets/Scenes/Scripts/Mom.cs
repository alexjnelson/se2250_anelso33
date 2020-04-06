using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mom : MonoBehaviour
{
    protected Vector3 change, currentPosition;
    Rigidbody2D myRigidbody;
    Animator animator;
    public float speed = 8;

    void Start(){
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // similar to enemy script, tracks player location and moves towards
    void Update()
    {   
        currentPosition = transform.position;
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
        
        if (PlayerMovement.instance.levelsCleared == 3){
            MoveToPlayer(PlayerMovement.instance.gameObject.transform.position);
        }
    }

    void MoveToPlayer(Vector3 playerPosition) {
        change = Vector3.Normalize(playerPosition - currentPosition);

        if (change != Vector3.zero && Vector3.Distance(playerPosition, currentPosition) > 3) {
            myRigidbody.MovePosition(currentPosition + change * speed * Time.deltaTime);
            animator.SetBool("moving", true);
        }
        else{
            animator.SetBool("moving", false);
        }
    }
}
