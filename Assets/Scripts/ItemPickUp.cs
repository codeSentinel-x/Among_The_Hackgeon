using UnityEngine;

public enum ItemType {
    Ammo,
    Healing,
    Weapon,
    Blank,
    Special,
    Key,
}
public class ItemPickUp : MonoBehaviour {
    public string _name;
    public int _amount;
    public ItemType _itemType;
    public Transform _pickupParticle;
    void Awake() {
        if (_itemType == ItemType.Weapon) {
            GetComponent<SpriteRenderer>().sprite = AssetManager.LoadWeaponByName(_name)._sprite;
        }
    }
    void Start() {
        if (_itemType == ItemType.Weapon) {
            AssetManager.LoadWeaponByName(_name);
        } else if (_itemType == ItemType.Special) {
            GetComponent<TooltipShower>().Setup(AssetManager.LoadItemByName(_name));
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var p = other.gameObject.GetComponent<PlayerController>();
            p._currentItemInRange = this;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var p = other.gameObject.GetComponent<PlayerController>();
            if (p._currentItemInRange == this) p._currentItemInRange = null;
        }
    }
    void OnDestroy() {
        _ = Instantiate(_pickupParticle, transform.position, Quaternion.identity);
    }

}
public class Source<T> {
    public T value;
}
