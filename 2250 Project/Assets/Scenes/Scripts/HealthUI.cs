using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is responsible for gametime overlay, including health and level clearances
public class HealthUI : MonoBehaviour
{
    public GameObject levelBox;
    // constantly updates the on-screen health slider, as well as the text indicating completed levels
    void Update()
    {
        try {
            GetComponent<Slider>().value = PlayerMovement.instance.gameObject.GetComponent<Health>().health / 100f;
            levelBox.GetComponent<Text>().text = "Levels Completed:" + PlayerMovement.instance.levelsCleared + "/3";
        }
        catch {
            
        }
        
    }
}
