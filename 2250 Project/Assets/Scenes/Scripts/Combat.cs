using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject AttackHitBox, AttackHitBoxRanged;
    private Animator anim;
    public Vector3 facingDirection;
    private double _attackCooldownTime;
    public double attackCooldown;
    private float rangeX, rangeZ;

    private BattleItemScript _item;


    void Start(){
        attackCooldown = 0;
        anim = PlayerMovement.instance.animator;
    }

    // update determines the parameters of the gameobject's attack as they may be subject to change when swapping weaponry
    // this method also handles cooldowns on attacks; an attack cannot be made while the cooldown is above 0
    // finally, this method determines the direction in which an attack was made
    void Update(){
         _item = gameObject.GetComponent<BattleItemScript>();
        rangeX = _item.item.rangeX;
        rangeZ = _item.item.rangeZ;
        // since an item's attack speed is the amount of times it can hit per second, the cooldown timer is the inverse
        _attackCooldownTime = 1.0 / _item.item.attackSpeed; 

        attackCooldown = attackCooldown <= 0 ? 0 : attackCooldown - Time.deltaTime;
        facingDirection = gameObject.CompareTag("Player") ? GetComponent<PlayerMovement>().facingDirection : GetComponent<Enemy>().facingDirection;
    }

    // this method handles melee attacks. It sets the size of the hitbox and hitbox damage according to weapon parameteres, and
    // tells the hitbox what type of attack it is using tags
    public void Attack()
    {
        if (attackCooldown <= 0){
            GameObject attack = Instantiate(AttackHitBox, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
            attack.GetComponent<AttackCollider>().damage = _item.item.damage * GetComponent<Stats>().attack;
            attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, rangeZ, 1);
            attack.GetComponent<AttackCollider>().attackTime = _attackCooldownTime;
            attack.tag = gameObject.CompareTag("Player") ? "PlayerAttack" : "EnemyAttack";

            anim.speed = 0.43f/(float)_attackCooldownTime; // this changes the speed of the attack animation so it lasts as long as the attack
            attackCooldown = _attackCooldownTime; // begins counting down the cooldown timer
        }
    }

    // the same as above, but for a ranged attack. This also determines the speed of projectiles and tells the hitbox
    // what direction is should move in.
    public void AttackRanged(){
        if (attackCooldown <= 0){
            GameObject attack = Instantiate(AttackHitBoxRanged, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
            AttackColliderRanged hitbox = attack.GetComponent<AttackColliderRanged>();
            
            hitbox.damage = _item.item.damage * GetComponent<Stats>().attack;
            hitbox.speed = 20;
            hitbox.direction = facingDirection;

            attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, rangeZ, 1);
            attack.tag = gameObject.CompareTag("Player") ? "PlayerAttack" : "EnemyAttack";

            attackCooldown = _attackCooldownTime;
        }
    }



}
