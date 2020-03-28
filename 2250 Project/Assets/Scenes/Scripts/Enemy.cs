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
    private float _trackingDistance = 8;
    private double _wanderTimer;
    
    //private Animator animator;

    public Vector3 facingDirection;
    private Vector3 change, wanderDirection;
    
    void Start()
    {
        _item = gameObject.GetComponent<BattleItemScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        gameObject.tag="Enemy";

        _wanderTimer = 0;
    }

    
    void Update()
    {
        playerScript = PlayerMovement.instance;
        Vector3 playerPosition = playerScript.gameObject.transform.position;

        if (Vector3.Distance(playerPosition, transform.position) < _trackingDistance){
            MoveToPlayer(playerPosition);
            _wanderTimer = 0;
        }
        else {
            Wander();
        }
    }

    void Wander(){
        if (_wanderTimer <= 0){
            _wanderTimer = Random.Range(2.0f, 6.0f);
            wanderDirection = Vector3.Normalize(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0));
            facingDirection = wanderDirection;
            print(wanderDirection);
        }
        else {
            _wanderTimer -= Time.deltaTime;
            myRigidbody.MovePosition(transform.position + wanderDirection * speed * Time.deltaTime);
        }
    }

    void MoveToPlayer(Vector3 playerPosition)
    {
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
            // animator.SetBool("moving", false);
        }
    }

    protected virtual bool CheckAttack(Vector3 playerPosition) {
        if (Vector3.Distance(playerPosition, transform.position) <  1.3){
            GetComponent<Combat>().Attack();
            return true;
        }
        else { return false; }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (_wanderTimer > 0) { 
            _wanderTimer = 0; 
             myRigidbody.MovePosition(transform.position - wanderDirection * speed * 0.75f * Time.deltaTime);
        }
    }

}