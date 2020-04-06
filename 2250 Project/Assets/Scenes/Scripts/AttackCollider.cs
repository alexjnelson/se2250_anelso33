using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is the hitbox which spawns upon a melee attack. It is also the base class of a ranged attack hitbox.
public class AttackCollider : MonoBehaviour
{
    public float damage;
    public double attackTime;
    public Animator animator;

    // it is necessary to obtain the player animator so the attack animation can be adjusted according to the length of the attack
    protected virtual void Start(){
        animator = PlayerMovement.instance.animator; 
        if (CompareTag("PlayerAttack")) { 
            animator.SetBool("attacking", true);
        }
    }

    // a timer runs while the attack animation plays; when it is complete, the attack hitbox destroys itself
    protected virtual void Update(){
        attackTime -= Time.deltaTime;
        if (CompareTag("PlayerAttack")) { PlayerMovement.instance.lockMovement = true; }

         if (attackTime <= 0 ){
            Destroy(gameObject);
        }
    }

    // if an adversary (enemy if the player attacked; player if the enemy attacked) is in the hitbox, they are dealth damage
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
