using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0 : MonoBehaviour
{
    private string _storyText ="You hear a muffled shout from the first floor. Quick, get changed! (Go to dresser). You need to investigate.";
    void Start (){
        GameObject.Find("MenuOverlay").GetComponent<PauseMenu>().ShowStory(_storyText);
    }
}
