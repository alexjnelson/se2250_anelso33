using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void addItem(Item item){
        this.items.Add(item);
    }

    public void removeItem(Item item){
        this.items.Remove(item);
        this.items.TrimExcess();
    }
}
