using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject levelBox;
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
