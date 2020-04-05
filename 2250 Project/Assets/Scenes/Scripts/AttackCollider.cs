using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage;
    public double attackTime;
    public Animator animator;

    protected virtual void Start(){
        animator = PlayerMovement.instance.animator;
        if (CompareTag("PlayerAttack")) { 
            animator.SetBool("attacking", true);
        }
    }

    protected virtual void Update(){
        attackTime -= Time.deltaTime;
        if (CompareTag("PlayerAttack")) { PlayerMovement.instance.lockMovement = true; }

         if (attackTime <= 0 ){
            Destroy(gameObject);
        }
    }

    virtual protected void OnTriggerEnter2D (Collider2D collision)
    {
        if ((gameObject.tag == "PlayerAttack" && collision.gameObject.tag =="Enemy") || (gameObject.tag == "EnemyAttack" && collision.gameObject.tag =="Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage/collision.gameObject.GetComponent<Stats>().defense);
        }
    }

    protected void OnDestroy(){
        if (CompareTag("PlayerAttack")) { 
            animator.SetBool("attacking", false);
            animator.speed = 1f; // resets animator speed
            PlayerMovement.instance.lockMovement = false; 
        }
    }
}
