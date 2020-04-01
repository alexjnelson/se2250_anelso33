﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ExpBar : ScriptableObject
{
    public int experience = 0;

    public void GainExperience(int experience)
    {
        if ((this.experience += experience) > GetThreshold(PlayerMovement.instance.level))
        {
            PlayerMovement.instance.LevelUp();
        }
    }

    /*
    Recursive algorithm causing the first level to be gained at 4exp, and subsequent levels require an additional 2exp to
    be gained per level such that the thresholds are { 4, 10, 18, 28, 40, 64 ... } total exp
    */
    private int GetThreshold(int level){
        if (level == 0) { return 4; }
        else { return 2*level + 4 + GetThreshold(level-1); }
    }
}
