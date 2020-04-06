using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

// this is the enemy base class and represents melee fighters by default
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
    public GameObject groundItemPrefab;
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
        if (droppedItem == null){ GenerateDroppedItem(); } // only generates a dropped item if one is not predetermined

        _wanderTimer = 0;
    }

    // RNG's the enemy's dropped item. There is a 10% change it is a big potion, 20% change small potion, and 1% chance for a level orb
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

    // this primarily handles movement. The enemy finds its current position and the player's position, and if the player is close enough,
    // the enemy tracks towards them. If not, the enemy wanders in a random direction
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

    // this generates the random direction and determines how long it will move in that direction. Wandering can be stopped if the 
    // player comes into range; in that case, the wandering resets.
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

    // if the enemy is tracking, this moves them in the direction of the player. It checks if the player is in range to attack; if so, the enemy stays still
    void MoveToPlayer(Vector3 playerPosition)
    {
        change = Vector3.Normalize(playerPosition - currentPosition);

        if (CheckAttack(playerPosition)){
            facingDirection = change;
        }
        else if (change != Vector3.zero) {
            myRigidbody.MovePosition(currentPosition + change * speed * Time.deltaTime);
            facingDirection = change;
        }
    }

    // if the player is in range to be attacked, an attack hitbox is generated through the combat script
    protected virtual bool CheckAttack(Vector3 playerPosition) {
        if (Vector3.Distance(playerPosition, currentPosition) <  attackRange && GameObject.FindWithTag("Player") != null){
            GetComponent<Combat>().Attack();
            return true;
        }
        else { return false; }
    }

    // if an enemy runs into a wall while wandering, it moves away from the wall and then the wander is reset so a new direction can be RNG'd
    void OnCollisionEnter2D (Collision2D collision) {
        if (_wanderTimer > 0) { 
            _wanderTimer = 0; 
             myRigidbody.MovePosition(currentPosition - wanderDirection * speed * 0.75f * Time.deltaTime);
        }
    }

    // if the enemy dies, it drops its generated drop item and gives the player exp based on the enemy class
    virtual protected void OnDestroy(){
        if (GetComponent<Health>().health<=0){ // only does death actions if enemy was killed, not on reload
            if (droppedItem != null) { 
                GameObject groundItem = Instantiate(groundItemPrefab, transform.position, Quaternion.identity);
                groundItem.GetComponent<GroundItem>().item = droppedItem;
            }
            playerScript.gameObject.GetComponent<ExpBar>().GainExperience(expDropped);
        }
    }

}