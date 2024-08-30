using MyUtils.Classes;
using UnityEngine;

public class Mouse_DraggableItemFromInventory : MonoBehaviour {
    public static Mouse_DraggableItemFromInventory _I;
    public DraggableSlot _itemSlotToDragPF;
    public DraggableSlot _dSlot;
    public bool _hasItem;

    private void Awake() {
        _I = this;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }
    public void SetUp(Item itemToDrag) {
        _dSlot = Instantiate(_itemSlotToDragPF, this.transform);
        _dSlot.Setup(itemToDrag);
        Inventory_UI._onInventoryClose += RestartSlot;
        _hasItem = true;
    }
    public void SetUp(Item itemToDrag, int amount) {
        if (_dSlot != null) Destroy(_dSlot.gameObject);
        _dSlot = Instantiate(_itemSlotToDragPF, this.transform);
        _dSlot.Setup(itemToDrag, amount);
        Inventory_UI._onInventoryClose += RestartSlot;
        _hasItem = true;
    }
    public void ChangeItem(Item newItem, out Item oldItem) {
        oldItem = _dSlot._currentItem;
        Destroy(_dSlot.gameObject);
        _dSlot = Instantiate(_itemSlotToDragPF, this.transform);
        _dSlot.Setup(newItem);
        _hasItem = true;
    }
    public void RestartSlot() {
        if (_dSlot != null) {
            if (_dSlot._currentItem != null) PlayerInventory._I._inventory.GetEmptySlot().AddItem(_dSlot._currentItem);
        }
        if (this != null) Destroy(this.gameObject);
    }
    public void Reset() {
        Inventory_UI._onInventoryClose -= RestartSlot;
        Destroy(this.gameObject);
    }
    private void Update() {
        UpdatePosition();
    }
    void UpdatePosition() {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;

    }

    public void DecreaseAmountByOne() {
        _dSlot._currentItem._amount--;
        _dSlot.RefreshTXT();
    }
}
