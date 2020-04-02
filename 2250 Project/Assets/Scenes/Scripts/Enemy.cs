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
    public float trackingDistance;
    private double _wanderTimer;

    public Item droppedItem;
    public Item LevelOrb, SmallPotion, BigPotion;
    public int expDropped;
    
    //private Animator animator;

    public Vector3 facingDirection;
    protected Vector3 change, wanderDirection, currentPosition;

    public float attackRange;
    
    void Start()
    {
        _item = gameObject.GetComponent<BattleItemScript>();
        myRigidbody = GetComponent<Rigidbody2D>();
        gameObject.tag="Enemy";
        GenerateDroppedItem();

        _wanderTimer = 0;
    }

    virtual protected void GenerateDroppedItem (){ // can be overridden for boss drops, etc
        int r = Random.Range(0, 99);

        if (r == 99){
            droppedItem = LevelOrb;
        }
        else if (r < 10){
            droppedItem = BigPotion;
        }
        else if (r < 30){
            droppedItem = SmallPotion;
        }
    }

    
    void Update()
    {
        playerScript = PlayerMovement.instance;
        currentPosition = GetComponent<BoxCollider2D>().transform.position;
        Vector3 playerPosition = GameObject.FindWithTag("Player") != null ? playerScript.gameObject.transform.position : currentPosition;

        if (Vector3.Distance(playerPosition, currentPosition) < trackingDistance){
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
        }
        else {
            _wanderTimer -= Time.deltaTime;
            myRigidbody.MovePosition(currentPosition + wanderDirection * speed * Time.deltaTime);
        }
    }

    void MoveToPlayer(Vector3 playerPosition)
    {
        change = Vector3.Normalize(playerPosition - currentPosition);

        if (CheckAttack(playerPosition)){
            facingDirection = change;
        }
        else if (change != Vector3.zero) {
            myRigidbody.MovePosition(currentPosition + change * speed * Time.deltaTime);
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
        if (Vector3.Distance(playerPosition, currentPosition) <  attackRange && GameObject.FindWithTag("Player") != null){
            GetComponent<Combat>().Attack();
            return true;
        }
        else { return false; }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (_wanderTimer > 0) { 
            _wanderTimer = 0; 
             myRigidbody.MovePosition(currentPosition - wanderDirection * speed * 0.75f * Time.deltaTime);
        }
    }

    void OnDestroy(){
        if (droppedItem != null) { playerScript.gameObject.GetComponent<Bag>().addItem(droppedItem); }
        playerScript.gameObject.GetComponent<ExpBar>().GainExperience(expDropped);
    }

}