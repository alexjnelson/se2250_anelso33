using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused, viewingBag, viewingSkills, viewingSaves, viewingLoads, viewingStory;
    public static string storyTextString;
    public GameObject pauseMenuUI, bag, skillUpgrades, attackTextBox, defenseTextBox, skillPointsTextBox, healthUI, 
        saveManager, loadManager, saveDeny, storyBoard, storyText;

    public Transform itemsContainer;
    public GameObject [] slots;
    
    public Stats playerStats;
    public PlayerMovement player;
    public GameObject save0, save1, save2;

    void Start(){
        paused = false;
        viewingBag = false;
        viewingSkills = false;
        viewingSaves = false;
        viewingLoads = false;
        pauseMenuUI.SetActive(false);
        bag.SetActive(false);
        skillUpgrades.SetActive(false);
        saveManager.SetActive(false);
        loadManager.SetActive(false);
        saveDeny.SetActive(false);
        healthUI.SetActive(true);

        if (viewingStory){
            paused = true;
            UpdateStoryText();
            storyBoard.SetActive(true);
        }

        slots = new GameObject [24];
        Transform [] slotChildren = itemsContainer.GetComponentsInChildren<Transform>();

        int i = 0;
        foreach (Transform child in slotChildren){
            if (child.parent==itemsContainer){
                slots[i++] = child.gameObject;
            }
        }

        player = PlayerMovement.instance;
        playerStats = player.gameObject.GetComponent<Stats>();
    }

    void Update()
    {
        if (player == null){
            player = PlayerMovement.instance;
            playerStats = player.gameObject.GetComponent<Stats>();
        }

        if (viewingStory){
            UpdateStoryText();
            storyBoard.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (paused && !viewingBag && !viewingSkills && !viewingSaves && !viewingLoads){
                Resume();
            }
            else if (viewingBag){
                CloseBag();
            }
            else if (viewingSkills){
                CloseSkills();
            }
            else if (viewingSaves){
                CloseSaves();
            }
            else if (viewingLoads){
                CloseLoading();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume(){
        healthUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        saveDeny.SetActive(false);
        storyBoard.SetActive(false);
        viewingStory = false;
        Time.timeScale = 1f;
        paused = false;
    }

    void Pause(){
        healthUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void FillBag(){
        List<Item> playerItems = player.gameObject.GetComponent<Bag>().items;

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
        bag.SetActive(false);
        viewingBag = false;
    }

    public void SetStats(){
        attackTextBox.GetComponent<Text>().text = playerStats.attack.ToString();
        defenseTextBox.GetComponent<Text>().text = playerStats.defense.ToString();
        skillPointsTextBox.GetComponent<Text>().text = "Skill Points Remaining: " + player.skillTokens;
    }

    public void OpenSkills(){
        SetStats();

        viewingSkills = true;
        skillUpgrades.SetActive(true);
    }

    public void CloseSkills(){
        skillUpgrades.SetActive(false);
        viewingSkills = false;
    }

    public void UpgradeAttack(){
        if (player.skillTokens > 0){
            playerStats.attack += 0.5f;
            player.skillTokens--;
            SetStats();
        }
    }

    public void UpgradeDefense(){
        if (player.skillTokens > 0){
            playerStats.defense += 0.5f;
            player.skillTokens--;
            SetStats();
        }
    }

    public void OpenSaves(){
        if (player.allowExit){
            viewingSaves = true;
            saveManager.SetActive(true);
        }
        else { saveDeny.SetActive(true); }
    }

    public void CloseSaves(){
        viewingSaves = false;
        saveManager.SetActive(false);
    }

    public void OpenLoading(){
        viewingLoads = true;
        loadManager.SetActive(true);
    }

    public void CloseLoading(){
        viewingLoads = false;
        loadManager.SetActive(false);
    }

    public void Save(int saveNumber){
        player.Save(saveNumber);
        player.animator.SetBool("changedClothes", player.outfit==1);
    }

    public void Load(int saveNumber){
        CameraMovement.playerSave = saveNumber == 0 ? save0 : saveNumber == 1 ? save1 : save2;
        Application.LoadLevel(CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared);
        Resume();
    }

    public void ShowStory(string text){
        storyTextString = text; 
        storyBoard.SetActive(true);
        viewingStory = true;

        Time.timeScale = 0f;
        paused = true;
    }

    public void UpdateStoryText(){
        storyText.GetComponent<Text>().text = storyTextString;
    }

}
