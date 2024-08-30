using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory_UI : MonoBehaviour {
    public static Action onInventoryClose;
    public Action onEquipmentChange;
    private Inventory inventory;
    public List<InventorySlot> slots = new();
    public SpecialInventorySlot[] specialSlots;
    public GameObject specialSlotParent;
    public GameObject slotParent;
    public GameObject slotPF;
    private void Awake() {
        slotParent.GetComponentsInChildren<InventorySlot>().ToList().ForEach(x => slots.Add(x));
        specialSlots = specialSlotParent.GetComponentsInChildren<SpecialInventorySlot>();
    }
    public void SetInventory(Inventory invent) {
        inventory = invent;
        inventory.OnInventorySizeChange += ChangeInventorySize;
        SetupInventorySlots();
        SetupSpecialInventorySlots();
        ChangeInventorySize();
    }
    public void SetupInventorySlots() {
        foreach (InventorySlot slot in slots) {
            slot.Setup(inventory);
        }
    }
    public void SetupSpecialInventorySlots() {
        foreach (SpecialInventorySlot slot in specialSlots) {
            slot.Setup(inventory);
        }
    }

    public void AddInventorySlot() {
        while (inventory.inventorySize > slots.Count) {
            InventorySlot slot = Instantiate(slotPF, slotParent.transform).GetComponent<InventorySlot>();
            slot.Setup(inventory);
            slots.Add(slot);

        }
    }


    private void ChangeInventorySize() {
        if (slots.Count > inventory.inventorySize) RemoveInventorySlot(slots.Count - inventory.inventorySize);
        else AddInventorySlot();
    }
    public void RemoveInventorySlot(int amountToRemove) {
        if (amountToRemove > inventory.inventorySize) amountToRemove = inventory.inventorySize - 1;

        List<InventorySlot> emptySlots = inventory.GetEmptySlots();
        foreach (InventorySlot slot in emptySlots) {
            InventorySlot slotToRemove = slot;
            slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);
            amountToRemove -= 1;
            if (amountToRemove == 0) return;
        }
        slots.Reverse();
        for (int i = 0; i < amountToRemove; i++) {
            InventorySlot slotToRemove = slots[i];
            if (!slotToRemove.isEmpty) {
                inventory.RemoveItem(slotToRemove.item);
                TossItem(slotToRemove.item);
            }
            //?    slotToRemove.RemoveItem();
            slots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);

        }
        slots.Reverse();
    }

    public void TossItem(Item item) {
        //TODO Toss Item 
        //throw new System.NotImplementedException();
    }


}
