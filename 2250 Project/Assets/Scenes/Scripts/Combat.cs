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
    }

    void Update(){
         _item = gameObject.GetComponent<BattleItemScript>();
        rangeX = _item.item.rangeX;
        rangeZ = _item.item.rangeZ;
        _attackCooldownTime = 1.0 / _item.item.attackSpeed;

        attackCooldown = attackCooldown <= 0 ? 0 : attackCooldown - Time.deltaTime;
        facingDirection = gameObject.CompareTag("Player") ? GetComponent<PlayerMovement>().facingDirection : GetComponent<Enemy>().facingDirection;
    }

    public void Attack()
    {
        if (attackCooldown <= 0){
            GameObject attack = Instantiate(AttackHitBox, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
            attack.GetComponent<AttackCollider>().damage = _item.item.damage;
            attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, 1, rangeZ);
            attack.tag = gameObject.CompareTag("Player") ? "PlayerAttack" : "EnemyAttack";

            attackCooldown = _attackCooldownTime;
        }
    }

    public void AttackRanged(){
        if (attackCooldown <= 0){
            GameObject attack = Instantiate(AttackHitBoxRanged, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
            AttackColliderRanged hitbox = attack.GetComponent<AttackColliderRanged>();
            
            hitbox.damage = _item.item.damage/2;
            hitbox.speed = 20;
            hitbox.direction = facingDirection;

            attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, 1, rangeZ);
            attack.tag = gameObject.CompareTag("Player") ? "PlayerAttack" : "EnemyAttack";

            attackCooldown = _attackCooldownTime;
        }
    }



}
