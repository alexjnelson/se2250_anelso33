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

    public void Load(int saveNumber){
        CameraMovement.playerSave = saveNumber == 0 ? save0 : saveNumber == 1 ? save1 : save2;
        if (CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared==0){
            saveDataMessage.SetActive(true);
        }
        else {
            viewingLoads = false;
            saveDataMessage.SetActive(false);
            Application.LoadLevel(CameraMovement.playerSave.GetComponent<PlayerMovement>().levelsCleared+1);
        }
    }
}
