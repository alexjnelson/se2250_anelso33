using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script handles equiped weaponry for both players and enemies
public class BattleItemScript : MonoBehaviour
{
    public BattleItem battleItem;

    // if a battle item is already set, its sprite must be changed to inactive, and then the new battle item can be set
    public void setItem(BattleItem newItem){
        if (this.battleItem!=null) { this.battleItem.itemSprite = this.battleItem.inactive; }
        this.battleItem = newItem;
    }

    // this method is only applicable to players removing any equiped item
    public void removeItem(){
        GetComponent<PlayerMovement>().ResetItem();
    }

    // when the battle item is called, if no battle item exists, a default one is returned instead (see BattleItem constructor)
    public BattleItem item {
        get {
            return this.battleItem != null ? this.battleItem : new BattleItem();
        }
    }
}
