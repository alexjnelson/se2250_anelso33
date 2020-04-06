using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

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

    // instance allows player to be called from any script
    void Start()
    {
        instance = this;
        myRigidbody = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();
        animator.SetBool("changedClothes", outfit==1); // determines if the player had changed clothes
    }

    void Update() {
        // resets movement direction vector, then accepts a value based on keyboard input (either WASD or arrow keys)
        change = Vector3.zero; 

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // checks for an attack instruction, and then checks the player's outfit to determine the attack type
        if (Input.GetKeyDown(KeyCode.Space) && outfit==0) {
            animator.SetBool("moving", false); // restricts movement in animation for melee attack
            change = Vector3.zero;
            gameObject.GetComponent<Combat>().Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && outfit==1) {
            gameObject.GetComponent<Combat>().AttackRanged();
        }
        // if movement is not locked, player may move
        if (outfit==1 || !lockMovement) { updateAnimationAndMove(); }

        // while left shift is held, player can sprint
        if (Input.GetKey("left shift")) {
            speed = defaultSpeed * 1.8f;
        }
        else {
            speed = defaultSpeed;
        }
    }

    // when a battle item is clicked in the bag, it deselects itself and must be reverted to a default item stored in this script
    public void ResetItem(){
        if (outfit == 0) { GetComponent<BattleItemScript>().setItem(basicMelee); }
        else { GetComponent<BattleItemScript>().setItem(basicRanged); }
    }

    // moves player and sets a public vector indicating the direction the player is facing; this is used for attacks.
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
    
    // performs movement
    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + Vector3.Normalize(change) * speed * Time.deltaTime);
    }

    // typically called from the EXP script, but can be called from LevelOrbs. Provides the player with a point to upgrade a skill upon leveling up
    public void LevelUp()
    {
        this.level++;
        this.skillTokens++;
        print("Level Up!");
    }

    // saves the current state of the player to be loaded in later; up to 3 different save files can be maintained
    public void Save(int saveNumber){
        if (saveNumber < 3){
            string path = "Assets/SaveFiles/" + saveNumber.ToString() + "/PlayerSave" + ".prefab";
            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, path, InteractionMode.UserAction);
        }
    }

    // when the player dies, a death message is shown and the main menu can be accessed
    void OnDestroy(){
        if (GetComponent<Health>().health<=0){ // only does death actions if player was killed, not on reload
            GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowDeathMessage();
        }
    }

}
