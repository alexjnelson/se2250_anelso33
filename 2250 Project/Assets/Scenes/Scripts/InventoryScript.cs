using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Item item;
    public Image icon;

    public GameObject menu;

    public void AddItem (Item newItem){
        this.item = newItem;
        this.icon.sprite = newItem.itemSprite;
        this.icon.enabled = true;
    }

    public void RemoveItem (){
        this.item = null;
        this.icon.sprite = null;
        this.icon.enabled = false;
    }

    public void UseItem(){
        this.item.Use();
        menu.GetComponent<PauseMenu>().FillBag();
    }

    
}
