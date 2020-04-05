using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    private string _storyText ="You hear a muffled shout from the first floor. Quick, get changed! (Go to dresser). You need to investigate.";
    private static bool gameBegan = false;
    void Start (){
        if (!gameBegan){
            GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
            gameBegan = true; 
        }
        
    }
}
