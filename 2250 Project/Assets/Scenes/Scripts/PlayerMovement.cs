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

    public Bag playerBag;
    public ExpBar expBar;
    public GameObject coords;
    
    public int level = 0, skillTokens = 0;

    public Vector3 facingDirection;
    public int outfit;

    public BattleItem basicMelee, basicRanged;

    public int levelsCleared = 0;
    public bool allowExit = true, lockMovement = false; // lock movement only applies to melee player

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerBag = new Bag();
        expBar = new ExpBar();

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (outfit==1 || !lockMovement) { updateAnimationAndMove(); }

        if (outfit == 0){
            GetComponent<BattleItemScript>().setItem(basicMelee);

            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Combat>().attackCooldown <= 0) {
                animator.SetBool("attacking", true);
                animator.Play("Attacking");
                gameObject.GetComponent<Combat>().Attack();
            }
            else
            {
                animator.SetBool("attacking", false);
            }
        }
        
        else if (outfit == 1){
            GetComponent<BattleItemScript>().setItem(basicRanged);
            
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameObject.GetComponent<Combat>().AttackRanged();
            }
        }

        if (Input.GetKey("left shift"))
        {
            speed = defaultSpeed * 1.8f;
        }
        else
        {
            speed = defaultSpeed;
        }
    }

    void updateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);

            facingDirection = change;
        }
        else
        {
            animator.SetBool("moving", false);
        }
        //print(change);
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + Vector3.Normalize(change) * speed * Time.deltaTime);
    }


    public void LevelUp()
    {
        this.level++;
        this.skillTokens++;
        print("Level Up!");
    }

}
