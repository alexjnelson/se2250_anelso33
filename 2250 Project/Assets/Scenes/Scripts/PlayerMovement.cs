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
    public float maxHealth = 100f;
    public float health;
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
        health = maxHealth;
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

        if (outfit == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Combat>().attackCooldown <= 0)
            {
                animator.SetBool("attacking", true);
                animator.Play("Attacking");
                gameObject.GetComponent<Combat>().Attack();
            }
            else
            {
                animator.SetBool("attacking", false);
            }
        }
        else if (outfit == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
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
