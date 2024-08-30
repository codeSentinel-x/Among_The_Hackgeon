using System;
using System.Collections.Generic;
using System.Linq;
using MyUtils.Classes;
using UnityEngine;

public class Inventory_UI : MonoBehaviour {
    public static Action _onInventoryClose;
    public Action _onEquipmentChange;
    private Inventory _inventory;
    public List<InventorySlot> _slots = new();
    public SpecialInventorySlot[] _specialSlots;
    public GameObject _specialSlotParent;
    public GameObject _slotParent;
    public GameObject _slotPF;
    private void Awake() {
        _slotParent.GetComponentsInChildren<InventorySlot>().ToList().ForEach(x => _slots.Add(x));
        _specialSlots = _specialSlotParent.GetComponentsInChildren<SpecialInventorySlot>();
    }
    public void SetInventory(Inventory invent) {
        _inventory = invent;
        _inventory.OnInventorySizeChange += ChangeInventorySize;
        SetupInventorySlots();
        SetupSpecialInventorySlots();
        ChangeInventorySize();
    }
    public void SetupInventorySlots() {
        foreach (InventorySlot slot in _slots) {
            slot.Setup(_inventory);
        }
    }
    public void SetupSpecialInventorySlots() {
        foreach (SpecialInventorySlot slot in _specialSlots) {
            slot.Setup(_inventory);
        }
    }

    public void AddInventorySlot() {
        while (_inventory.inventorySize > _slots.Count) {
            InventorySlot slot = Instantiate(_slotPF, _slotParent.transform).GetComponent<InventorySlot>();
            slot.Setup(_inventory);
            _slots.Add(slot);

        }
    }


    private void ChangeInventorySize() {
        if (_slots.Count > _inventory.inventorySize) RemoveInventorySlot(_slots.Count - _inventory.inventorySize);
        else AddInventorySlot();
    }
    public void RemoveInventorySlot(int amountToRemove) {
        if (amountToRemove > _inventory.inventorySize) amountToRemove = _inventory.inventorySize - 1;

        List<InventorySlot> emptySlots = _inventory.GetEmptySlots();
        foreach (InventorySlot slot in emptySlots) {
            InventorySlot slotToRemove = slot;
            _slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);
            amountToRemove -= 1;
            if (amountToRemove == 0) return;
        }
        _slots.Reverse();
        for (int i = 0; i < amountToRemove; i++) {
            InventorySlot slotToRemove = _slots[i];
            if (!slotToRemove._isEmpty) {
                _inventory.RemoveItem(slotToRemove._item);
                TossItem(slotToRemove._item);
            }
            //?    slotToRemove.RemoveItem();
            _slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);

        }
        _slots.Reverse();
    }

    public void TossItem(Item item) {
        //TODO Toss Item 
        //throw new System.NotImplementedException();
    }


}
