using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject 
{
    public string itemName;
    public Sprite itemSprite; 

    public virtual void Use(){
        // placeholder; this is overriden in specific objects
    }

}
