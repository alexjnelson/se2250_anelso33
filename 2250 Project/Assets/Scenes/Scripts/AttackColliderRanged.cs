using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderRanged : AttackCollider
{
    public Vector3 direction;
    private Rigidbody2D myRigidbody;
    public int speed;

    override protected void Start(){
        attackTime = 3; // all ranged attacks stay on the map for 3 seconds
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (CompareTag("PlayerAttack")){
             animator = PlayerMovement.instance.animator;
            animator.SetBool("attacking", false); // no animation is required if the player is a ranged attacker
        }
       
    }

    // overriden update method calls its base method for the timing aspects, then moves itself  based on its speed
    override protected void Update(){
        base.Update();
        moveObject();
    }

    // direction of movement is taken from the direction the direction of the user who launched the attack
    void moveObject(){
        myRigidbody.MovePosition(transform.position + Vector3.Normalize(direction) * speed * Time.deltaTime);
    }

    // overrides so this hitbox destroys itself upon contact with a solid object (non-trigger, non-enemy)
    override protected void OnTriggerEnter2D (Collider2D collision){
        base.OnTriggerEnter2D(collision);
        if (!collision.isTrigger && !collision.CompareTag("Enemy")){
             Destroy(gameObject);
        }
       
    }
}
