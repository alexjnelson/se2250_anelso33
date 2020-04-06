using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemScript : MonoBehaviour
{
    public BattleItem battleItem;

    public void setItem(BattleItem newItem){
        if (this.battleItem!=null) { this.battleItem.itemSprite = this.battleItem.inactive; }
        this.battleItem = newItem;
    }

    public void removeItem(){
        GetComponent<PlayerMovement>().ResetItem();
    }

    public BattleItem item {
        get {
            return this.battleItem != null ? this.battleItem : new BattleItem();
        }
    }
}
