using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customization : MonoBehaviour
{


    private bool _playerInZone, _correctOutfit;
    public GameObject pressX;
    public PlayerMovement player;
    private Animator _animator;
    public string[] outfits = { "Idle", "IdleChangedClothes" };
    public int outfitsPicker = 0;

    public BattleItem basicMelee, basicRanged;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        pressX.SetActive(false);
        _animator = player.GetComponent<Animator>();

        _correctOutfit = CameraMovement.playerSave==null || CameraMovement.playerSave.GetComponent<PlayerMovement>().outfit==0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_correctOutfit && _animator.GetCurrentAnimatorStateInfo(0).IsName(outfits[outfitsPicker])){
            _correctOutfit = true;
        }

        if (!_correctOutfit){
            _animator.Play(outfits[outfitsPicker]);
        }
        else {
            outfitsPicker = player.outfit;
        }
        

        if (_playerInZone)
        {
            pressX.SetActive(true);
            FollowObject(player);
            if (Input.GetKeyUp("x"))
            {
                _animator.Play(outfits[outfitsPicker]);
                outfitsPicker = (outfitsPicker+1)%2;
                player.outfit = outfitsPicker;

                player.gameObject.GetComponent<BattleItemScript>().item.Use(); // removes current item, if there is one, so it can be set Inactive
            }
        }
        else
        {
            pressX.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
        }
    }
    private void FollowObject(PlayerMovement other)
    {
        pressX.transform.position = Camera.main.WorldToScreenPoint(other.transform.position) + new Vector3(0, 30, 0);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
        }
    }
}
