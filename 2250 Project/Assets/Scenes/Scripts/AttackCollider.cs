using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public float damage;
    protected double _attackTime;

    protected virtual void Start(){
        _attackTime = 0.5;
    }

    protected virtual void Update(){
        _attackTime -= Time.deltaTime;
        if (CompareTag("PlayerAttack")) { PlayerMovement.instance.lockMovement = true; }

         if (_attackTime <= 0 ){
            if (CompareTag("PlayerAttack")) { PlayerMovement.instance.lockMovement = false; }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if ((gameObject.tag == "PlayerAttack" && collision.gameObject.tag =="Enemy") || (gameObject.tag == "EnemyAttack" && collision.gameObject.tag =="Player"))
        {
            collision.gameObject.GetComponent<Health>().Damage(damage/collision.gameObject.GetComponent<Stats>().defense);
        }
    }
}
