using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public static PlayerMovement instance;

    public const float defaultSpeed = 10;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public GameObject AttackHitBox, AttackHitBoxRanged;

    public Bag playerBag;
    public ExpBar expBar;
    public GameObject coords;
    private int _level = 0, _skillTokens = 0, _attack = 3, _defense = 3;

    public Vector3 facingDirection;
    public int outfit;

    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        updateAnimationAndMove();

        if (outfit == 0){
            if (Input.GetKeyDown(KeyCode.Space)) {
                animator.SetBool("attacking", true);
                animator.Play("Attacking");
                Attack();
            }
            else {
                animator.SetBool("attacking", false);
            }
        }
        else if (outfit == 1){
            if (Input.GetKeyDown(KeyCode.Space)) {
                AttackRanged();
            }
        }

        if (Input.GetKey("left shift")){
            speed = defaultSpeed*1.8f;
        }
        else {
            speed = defaultSpeed;
        }
    }

    void updateAnimationAndMove()
    {
        if (change != Vector3.zero) {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

            facingDirection = change;
        }
        else {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    void Attack()
    {
        BattleItemScript item = gameObject.GetComponent<BattleItemScript>();
        int rangeX = item.item.rangeX, rangeZ = item.item.rangeZ;

        GameObject attack = Instantiate(AttackHitBox, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
        attack.GetComponent<AttackCollider>().damage = item.item.damage;
        attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, 1, rangeZ);
        attack.tag = "PlayerAttack";
    }

    void AttackRanged(){
        BattleItemScript item = gameObject.GetComponent<BattleItemScript>();
        int rangeX = item.item.rangeX, rangeZ = item.item.rangeZ;

        GameObject attack = Instantiate(AttackHitBoxRanged, gameObject.transform.position + facingDirection, Quaternion.identity); //spawns a boxcollider in the attack range
        AttackColliderRanged hitbox = attack.GetComponent<AttackColliderRanged>();
        
        hitbox.damage = item.item.damage/2;
        hitbox.speed = 20;
        hitbox.direction = facingDirection;

        attack.GetComponent<BoxCollider2D>().size = new Vector3(rangeX, 1, rangeZ);
        attack.tag = "PlayerAttack";
    }




    public void LevelUp()
    {
        this._level++;
        this._skillTokens++;

        this._attack += _level;
        this._defense += _level;
    }

    public int GetLevel()
    {
        return this._level;
    }

    void OpenMenu()
    {

    }

    void SelectSkills()
    {

    }

    void OpenBag()
    {
        Debug.Log(playerBag.items);
    }
}
