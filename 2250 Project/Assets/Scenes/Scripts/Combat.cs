using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject AttackHitBox;
    private PlayerMovement playerScript;
    private Animator anim;


    void Start(){
        playerScript = PlayerMovement.instance;
        anim = playerScript.gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {

    }



}
