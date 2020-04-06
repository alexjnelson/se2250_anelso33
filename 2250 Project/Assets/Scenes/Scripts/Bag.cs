using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // this is the list storing all items collected by the player

    // when called, adds a specified item to the list
    public void addItem(Item item){
        this.items.Add(item);
    }

    // removes a specified item from the list (such as consumables) and removes the empty space
    public void removeItem(Item item){
        this.items.Remove(item);
        this.items.TrimExcess();
    }
}
