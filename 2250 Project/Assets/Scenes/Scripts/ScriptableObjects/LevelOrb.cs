using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelOrb : Item
{
    public int skillPoints;

    // this item provides the player with the specified number of skill points upon its use. It is a consumable so it removes itself from the bag upon use
    public override void Use(){
        PlayerMovement player = PlayerMovement.instance;
        player.skillTokens++;
        player.level++;

        player.gameObject.GetComponent<Bag>().removeItem(this);
    }
}
