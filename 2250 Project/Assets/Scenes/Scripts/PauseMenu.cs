using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool paused, viewingBag;
    public GameObject pauseMenuUI, bag;

    public Transform itemsContainer;
    GameObject [] slots;

    public Item potion;

    void Start(){
        paused = false;
        viewingBag = false;
        pauseMenuUI.SetActive(false);
        bag.SetActive(false);

        slots = new GameObject [24];
        Transform [] slotChildren = itemsContainer.GetComponentsInChildren<Transform>();

        int i = 0;
        foreach (Transform child in slotChildren){
            if (child.parent==itemsContainer){
                slots[i++] = child.gameObject;
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (paused && !viewingBag){
                Resume();
            }
            else if (viewingBag){
                CloseBag();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void FillBag(){
        List<Item> playerItems = PlayerMovement.instance.playerBag.items;

        for (int i = 0 ; i < 24 ; i++) {
            if (i < playerItems.Count){
                slots[i].gameObject.GetComponent<InventoryScript>().AddItem(playerItems[i]);
            }
            else{
                slots[i].gameObject.GetComponent<InventoryScript>().RemoveItem();
            }
        }
    }

    public void OpenBag(){
        FillBag();

        viewingBag = true;
        bag.SetActive(true);
    }

    public void CloseBag(){
        viewingBag = false;
        bag.SetActive(false);
    }

    public void OpenSkills(){

    }

    public void Save(){

    }
}
