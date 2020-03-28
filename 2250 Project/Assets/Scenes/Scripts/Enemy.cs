using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float speed;
    private PlayerMovement playerScript;
    private BattleItemScript _item;
    
    //private Animator animator;

    public Vector3 facingDirection;
    private Vector3 change;
    
    void Start()
    {
        _item = gameObject.GetComponent<BattleItemScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        gameObject.tag="Enemy";
        speed = 6;
    }

    
    void Update()
    {
        playerScript = PlayerMovement.instance;
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        Vector3 playerPosition = playerScript.gameObject.transform.position;
        change = Vector3.Normalize(playerPosition - transform.position);

        if (CheckAttack(playerPosition)){
            facingDirection = change;
        }
        else if (change != Vector3.zero) {
            myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
            // animator.SetFloat("moveX", change.x);
            // animator.SetFloat("moveY", change.y);
            // animator.SetBool("moving", true);

            facingDirection = change;
        }
        else {
            //animator.SetBool("moving", false);
        }
    }

    protected virtual bool CheckAttack(Vector3 playerPosition){
        if (Vector3.Distance(playerPosition, transform.position) <  1.3){
            GetComponent<Combat>().Attack();
            return true;
        }
        else { return false; }
    }

}