using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public Item item;
    public BattleItem battleItem;

    void Update() {
        if (item.GetType() == typeof(BattleItem)){ 
            battleItem = (BattleItem) item; 
            item.itemSprite = battleItem.inactive;
        }
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }

    void OnTriggerEnter2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            Destroy(gameObject); 
        }
    }

    void OnDestroy(){
        PlayerMovement.instance.gameObject.GetComponent<Bag>().addItem(item);
    }
}
