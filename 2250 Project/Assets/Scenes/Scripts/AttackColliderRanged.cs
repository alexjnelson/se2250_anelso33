using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackColliderRanged : AttackCollider
{
    public Vector3 direction;
    private Rigidbody2D myRigidbody;
    public int speed;

    override protected void Start(){
        attackTime = 3;
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        animator = PlayerMovement.instance.animator;
        animator.SetBool("attacking", false);
    }

    override protected void Update(){
        base.Update();
        moveObject();
    }

    void moveObject(){
        myRigidbody.MovePosition(transform.position + Vector3.Normalize(direction) * speed * Time.deltaTime);
    }

    override protected void OnTriggerEnter2D (Collider2D collision){
        base.OnTriggerEnter2D(collision);
        if (!collision.isTrigger && !collision.CompareTag("Enemy")){
             Destroy(gameObject);
        }
       
    }
}
