using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public const float defaultSpeed = 10;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public Animator animator;

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
        myRigidbody = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();
        animator.SetBool("changedClothes", outfit==1);
    }

    // Update is called once per frame
    void Update() {
        change = Vector3.zero;

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && outfit==0) {
            animator.SetBool("moving", false);
            change = Vector3.zero;
            gameObject.GetComponent<Combat>().Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && outfit==1) {
            gameObject.GetComponent<Combat>().AttackRanged();
        }

        if (outfit==1 || !lockMovement) { updateAnimationAndMove(); }


        if (Input.GetKey("left shift")) {
            speed = defaultSpeed * 1.8f;
        }
        else {
            speed = defaultSpeed;
        }

        if (Input.GetKey("p")){
            Save(0);
        }
    }

    public void ResetItem(){
        if (outfit == 0) { GetComponent<BattleItemScript>().setItem(basicMelee); }
        else { GetComponent<BattleItemScript>().setItem(basicRanged); }
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

    public void Save(int saveNumber){
        if (saveNumber < 3){
            string path = "Assets/SaveFiles/" + saveNumber.ToString() + "/PlayerSave" + ".prefab";
            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, path, InteractionMode.UserAction);
        }
    }

}
