using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject loadManager, saveDataMessage;
    private static bool viewingLoads;
    public GameObject save0, save1, save2;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (viewingLoads){
                CloseLoading();
            }
        } 
    }

    public void NewGame(){
        CameraMovement.playerSave = null;
        Application.LoadLevel(1);
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

    // when a load option is selected, it is first verified that any load data is stored there. If there is, this player instance is
    // sent to the Camera script where it is applied to the scene; the last level cleared by that player is loaded
    public void Load(int saveNumber){
        CameraMovement.playerSave = saveNumber == 0 ? save0 : saveNumber == 1 ? save1 : save2;
        if (CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared==0){
            saveDataMessage.SetActive(true);
        }
        else {
            viewingLoads = false;
            saveDataMessage.SetActive(false);
            PauseMenu.gameStarted = false;
            Application.LoadLevel(CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared+1);
        }
    }
}
