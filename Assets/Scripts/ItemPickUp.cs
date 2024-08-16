using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Ammo,
    Weapon,
    Special
}
public class ItemPickUp : MonoBehaviour {
    public string _name;
    public ItemType _itemType;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            switch (_itemType) {
                case ItemType.Weapon: {
                        var p = other.gameObject.GetComponent<PlayerController>();
                        p._currentItemInRange = this;
                        break;
                    }
                default: break;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            switch (_itemType) {
                case ItemType.Weapon: {
                        var p = other.gameObject.GetComponent<PlayerController>();
                        if (p._currentItemInRange == this) p._currentItemInRange = null;
                        break;
                    }
                default: break;
            }
        }
    }

}
