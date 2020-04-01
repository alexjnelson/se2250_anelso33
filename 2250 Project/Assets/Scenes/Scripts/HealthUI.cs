using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    void Update()
    {
        try {
            GetComponent<Slider>().value = PlayerMovement.instance.gameObject.GetComponent<Health>().health / 100f;
        }
        catch {
            // yerr
        }
        
    }
}
