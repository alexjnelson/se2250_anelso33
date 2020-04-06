using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public Item item;
    public BattleItem battleItem;

    // since the item sprite may be changed after initiation, it is determined in Update. 
    void Update() {
        // if the item is a battle item, ensure that its Inactive sprite is shown (active has a red mark and is used in the bag to identify the active item)
        if (item.GetType() == typeof(BattleItem)){ 
            battleItem = (BattleItem) item; 
            item.itemSprite = battleItem.inactive;
        }
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    // if the player walks over the item, it is collected and despawned
    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            Destroy(gameObject); 
        }
    }
    // this actually adds the item to the player's bag
    void OnDestroy(){
        PlayerMovement.instance.gameObject.GetComponent<Bag>().addItem(item);
    }
}
