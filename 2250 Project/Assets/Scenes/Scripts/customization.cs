using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customization : MonoBehaviour
{


    private bool _playerInZone;
    public GameObject pressX;
    public PlayerMovement player;
    private Animator _animator;

    public BattleItem basicMelee, basicRanged;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pressX.SetActive(false);
        _animator = player.GetComponent<Animator>();
    }

    // if the player presses x while in the customization zone, they change their outfit. The outfit variable
    // in the player script and the animator bool are set accordingly
    void Update()
    {
        if (_playerInZone)
        {
            pressX.SetActive(true);
            FollowObject(player);
            if (Input.GetKeyUp("x"))
            {
                player.outfit = (player.outfit+1)%2; // there are 2 outfits to cycle through
                _animator.SetBool("changedClothes", player.outfit==1);
                player.gameObject.GetComponent<BattleItemScript>().item.Use(); // removes current item, if there is one, so it can be set Inactive
            }
        }
        else
        {
            pressX.SetActive(false);
        }
    }

    // this shows a message if the player is able to change clothes
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
        }
    }
    // this makes the message appear above the player's head
    private void FollowObject(PlayerMovement other)
    {
        pressX.transform.position = Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0, 30, 0);
    }
    // this deactives the message to the player
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
        }
    }
}
