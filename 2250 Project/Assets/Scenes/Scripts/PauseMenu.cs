using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused, viewingBag, viewingSkills, viewingSaves, viewingLoads, viewingStory, viewingDeath, gameStarted = false;
    public static string storyTextString;
    public GameObject pauseMenuUI, bag, skillUpgrades, attackTextBox, defenseTextBox, skillPointsTextBox, healthUI, 
        saveManager, loadManager, saveDeny, storyBoard, storyText, deathMessage, saveDataMessage, saveSuccessMessage, saveFailedMessage;

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
        viewingDeath = false;
        pauseMenuUI.SetActive(false);
        deathMessage.SetActive(false);
        saveDataMessage.SetActive(false);
        saveSuccessMessage.SetActive(false);
        saveFailedMessage.SetActive(false);
        bag.SetActive(false);
        skillUpgrades.SetActive(false);
        saveManager.SetActive(false);
        loadManager.SetActive(false);
        saveDeny.SetActive(false);
        healthUI.SetActive(true);

        if (viewingStory){ // at the beginning of a scene, the story text is updated if this level hadn't been cleared yet
            paused = true;
            UpdateStoryText();
            if (SceneManager.GetActiveScene().name!="StartingRoom") { 
                storyBoard.SetActive(true); 
            }
        }

        if(!gameStarted){ // at game start, the pause menu is brought up
            Pause();
        }

        // initializes the inventory slots - there are 24 spaces for bag items. There aren't actually 24 enemies to drop items, though
        slots = new GameObject [24];
        Transform [] slotChildren = itemsContainer.GetComponentsInChildren<Transform>();

        int i = 0;
        foreach (Transform child in slotChildren){ // this ensures only slot objects are considered
            if (child.parent==itemsContainer){
                slots[i++] = child.gameObject;
            }
        }

        player = PlayerMovement.instance;
        playerStats = player.gameObject.GetComponent<Stats>();
    }

    void Update()
    {
        if (player == null && !viewingDeath){
            player = PlayerMovement.instance;
            playerStats = player.gameObject.GetComponent<Stats>();
        }

        if (viewingStory && SceneManager.GetActiveScene().name!="StartingRoom"){
            UpdateStoryText();
            storyBoard.SetActive(true);
        }
        if (!gameStarted){
            UpdateStoryText();
            Pause();
            gameStarted=true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (paused && !viewingBag && !viewingSkills && !viewingSaves && !viewingLoads && !viewingDeath){
                Resume();
            }
            else if (viewingDeath){
                ReturnToMainMenu();
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

        viewingDeath = false;
        deathMessage.SetActive(false); 
        
        Time.timeScale = 1f;
        paused = false;

        if (viewingStory && SceneManager.GetActiveScene().name=="StartingRoom"){
            storyBoard.SetActive(true);
            paused=true;
        }
        else{
            storyBoard.SetActive(false);
        }
        viewingStory = false;

    }

    void Pause(){
        healthUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    // this initializes the inventory when the bag is opened. Items in the bag are put into inventory slots, and when there are not enough
    // items in the bag to fill every slot, these subsequent slots are ensured to be empty
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

    // this, along with the following method, provide the interface for the player to upgrade their skills. If they have skill tokens,
    // the clicked-on skill will be upgraded by 0.5. A skill is a multiplier applied to either damage dealt or received (atk and def respectively)
    // stats are refreshed to update the GUI upon upgrade
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
        saveSuccessMessage.SetActive(false);
        saveFailedMessage.SetActive(false);
        saveManager.SetActive(false);
    }

    public void OpenLoading(){
        viewingLoads = true;
        loadManager.SetActive(true);
    }

    public void CloseLoading(){
        viewingLoads = false;
        saveDataMessage.SetActive(false);
        loadManager.SetActive(false);
    }

    // saves the player's data in the clicked-on slot. If the player has not completed level 1, data cannot be saved.
    public void Save(int saveNumber){
        if (player.levelsCleared>0){
            player.Save(saveNumber);
            player.animator.SetBool("changedClothes", player.outfit==1);
            saveSuccessMessage.SetActive(true);
        }
        else{
            saveFailedMessage.SetActive(true);
        }
    }

    // when a load option is selected, it is first verified that any load data is stored there. If there is, this player instance is
    // sent to the Camera script where it is applied to the scene; the last level cleared by that player is loaded
    public void Load(int saveNumber){
        CameraMovement.playerSave = saveNumber == 0 ? save0 : saveNumber == 1 ? save1 : save2;
        if (CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared==0){
            saveDataMessage.SetActive(true);
        }
        else {
            saveDataMessage.SetActive(false);
            Application.LoadLevel(CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared+1);
            Resume(); 
        }
    }

    // this displays the story text as the game progresses
    public void ShowStory(string text){
        storyTextString = text;  
        viewingStory = true;

        Time.timeScale = 0f;
        paused = true;
    }

    public void UpdateStoryText(){
        storyText.GetComponent<Text>().text = storyTextString;
    }

    public void ShowDeathMessage(){
        paused = true;
        Time.timeScale = 0f;

        viewingDeath = true;
        deathMessage.SetActive(true);
    }

    public void ReturnToMainMenu(){
        Resume();
        Application.LoadLevel(0);
    }

}
