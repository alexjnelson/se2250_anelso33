using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelOrb : Item
{
    public int skillPoints;

    public void Use(){
        PlayerMovement player = PlayerMovement.instance;
        player.skillTokens++;
        player.level++;
    }
}
