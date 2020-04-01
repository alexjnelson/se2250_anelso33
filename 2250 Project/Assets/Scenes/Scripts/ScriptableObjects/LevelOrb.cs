using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelOrb : Item
{
    public int skillPoints;

    public override void Use(){
        PlayerMovement player = PlayerMovement.instance;
        player.skillTokens++;
        player.level++;

        player.playerBag.removeItem(this);
    }
}
