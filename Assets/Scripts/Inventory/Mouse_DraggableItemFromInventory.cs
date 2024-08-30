using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class Mouse_DraggableItemFromInventory : MonoBehaviour {
    public static Mouse_DraggableItemFromInventory I;
    public DraggableSlot itemSlotToDragPF;
    public DraggableSlot dSlot;
    public bool hasItem;

    private void Awake() {
        I = this;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }
    public void SetUp(Item itemToDrag) {
        dSlot = Instantiate(itemSlotToDragPF, this.transform);
        dSlot.Setup(itemToDrag);
        Inventory_UI.onInventoryClose += RestartSlot;
        hasItem = true;
    }
    public void SetUp(Item itemToDrag, int amount) {
        if (dSlot != null) Destroy(dSlot.gameObject);
        dSlot = Instantiate(itemSlotToDragPF, this.transform);
        dSlot.Setup(itemToDrag, amount);
        Inventory_UI.onInventoryClose += RestartSlot;
        hasItem = true;
    }
    public void ChangeItem(Item newItem, out Item oldItem) {
        oldItem = dSlot.currentItem;
        Destroy(dSlot.gameObject);
        dSlot = Instantiate(itemSlotToDragPF, this.transform);
        dSlot.Setup(newItem);
        hasItem = true;
    }
    public void RestartSlot() {
        if (dSlot != null) {
            if (dSlot.currentItem != null) Player_inventory.I.inventory.GetEmptySlot().AddItem(dSlot.currentItem);
        }
        if (this != null) Destroy(this.gameObject);
    }
    public void Reset() {
        Inventory_UI.onInventoryClose -= RestartSlot;
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
        dSlot.currentItem.amount--;
        dSlot.RefreshTXT();
    }
}
