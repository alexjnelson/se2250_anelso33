using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script manages the inventory display, not the actual storage of player items. It is responsible for an individual inventory slot display
public class InventoryScript : MonoBehaviour
{
    public Item item;
    public Image icon;

    public GameObject menu;

    // this is called when the inventory is open; the specified item is added to this inventory slot and its sprite is displayed
    public void AddItem (Item newItem){
        this.item = newItem;
        this.icon.sprite = newItem.itemSprite;
        this.icon.enabled = true;
    }

    // this is used to clear an inventory slot if it would not otherwise be accessed because the bag list is does not contain enough items to fill each slot
    public void RemoveItem (){
        this.item = null;
        this.icon.sprite = null;
        this.icon.enabled = false;
    }

    // allows an item to be clicked on, as the inventory slot is a button. Calls the item's Use method and refreshes the inventory
    public void UseItem(){
        this.item.Use();
        menu.GetComponent<PauseMenu>().FillBag();
    }

    
}
