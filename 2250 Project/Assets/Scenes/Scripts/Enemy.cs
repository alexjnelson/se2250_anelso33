using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool _inCombat;
    public EnemyTrigger enemyTrigger;
    public int attackDamage = 20, rangeX = 2, rangeZ = 2;
    public GameObject AttackHitBox;

    public Vector3 facingDirection;
    
    void Start()
    {
        gameObject.tag="Enemy";
    }

    
    void Update()
    {

    }


    void Attack()
    {
        GameObject attack = Instantiate(AttackHitBox, gameObject.transform.position + facingDirection, Quaternion.identity); 
        attack.GetComponent<AttackCollider>().damage = attackDamage;
        attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, 1, rangeZ);
        attack.tag = "EnemyAttack";
    }


}