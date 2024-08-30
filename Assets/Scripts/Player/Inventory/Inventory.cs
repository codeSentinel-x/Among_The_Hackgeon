using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Inventory {
    public Action OnInventorySizeChange;
    public int inventorySize;
    public List<Item> itemList = new();
    public Inventory_UI inventoryUI;
    public Inventory(Inventory_UI inv, int invSize) //Inventory setup
    {
        itemList ??= new();     //this is if(itemList == null){ itemList = new List<Item>()} but written like a pro;
        inventoryUI = inv;
        inventorySize = invSize;
    }


    public bool AddItem(Item item, bool stackWithOther = true) {
        if (itemList.Count == inventorySize) return false;
        if (!item.itemBase.isStackable || !stackWithOther) {
            GetEmptySlot().AddItem(item);
            itemList.Add(item);
            return true;
        }
        else {
            int amountToAdd = item.amount;
            int amountLeft = amountToAdd;
            foreach (Item it in itemList) {
                if (it.itemBase == item.itemBase) {
                    if (it.amount != it.maxStack) {

                        it.amount = CheckAmountToAdd(it, amountToAdd, out int aLeft);
                        InventorySlot slot = FindSlotWithItem(it);
                        if (slot != null) {
                            slot.RefreshItemAmount(it.amount);
                        }
                        else {
                            Debug.Log("Slot is null");
                            continue;
                        }
                        amountLeft = aLeft;
                        if (amountLeft == 0) break;
                    }
                }
            }
            if (amountLeft > 0) {
                item.amount = amountLeft;
                AddItem(item, false);
            }
            return true;
        }
    }
    public void RemoveItem(Item item) {
        // TODO remove item from inventory
        throw new System.NotImplementedException();
    }


    public void IncreaseInventoryCapacity(int amount) {
        inventorySize += amount;
        OnInventorySizeChange?.Invoke();
    }
    public void DecreesInventoryCapacity(int amount) {
        inventorySize -= amount;
        if (inventorySize <= 0) inventorySize = 1;
        OnInventorySizeChange?.Invoke();
    }

    public bool IsEnoughInInventory(Item item, int amountToCheck) {
        return GetItemInInventoryAmount(item) <= amountToCheck;
    }
    public int GetItemInInventoryAmount(Item item) {
        int amount = 0;
        foreach (InventorySlot slot in FindSlotsWIthItem(item)) {
            amount += slot.item.amount;
        }
        return amount;
    }

    private List<InventorySlot> FindSlotsWIthItem(Item item) {
        List<InventorySlot> slots = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.item == item) {
                slots.Add(slot);
            }
        }
        return slots;
    }

    public bool AddItemAmount(Item item, out int overInventoryAmount) {
        //TODO Add Item Amount
        throw new System.NotImplementedException();
    }
    public int CheckAmountToAdd(Item item, int a, out int overStackAmount) {
        if (!item.itemBase.isStackable) {
            overStackAmount = a;
            return 1;
        }

        int amount = item.amount;
        int maxStack = item.itemBase.stackSize;
        int control = amount + a;

        if (control > maxStack) {
            overStackAmount = control - maxStack;
            amount = maxStack;
        }
        else {
            amount += a;
            overStackAmount = 0;
        }
        // Debug.Log("overStackAmount = " + overStackAmount);
        // Debug.Log("amount = " + amount);
        return amount;

    }
    public void ChangeItemAmount(Item item, int a) {
        //TODO change item amount
    }
    public void RemoveItemAmount(int a, out int additionalAmount) {
        //TODO Remove item Amount
        throw new NotImplementedException();
    }

    public void RefreshItemList() {
        itemList = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (!slot.isEmpty) itemList.Add(slot.item);
        }
    }

    public int GetSpaceInInventory() {
        return inventorySize - itemList.Count;
    }
    private List<InventorySlot> FindSlotsWithItemOfType(ItemBaseSO itemBase) {
        List<InventorySlot> slots = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.item.itemBase == itemBase) slots.Add(slot);
        }
        return slots;
    }
    private InventorySlot FindSlotWithItem(Item it) {
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.isEmpty) continue;
            if (slot.item == it) return slot;
        }
        return null;
    }
    public InventorySlot GetEmptySlot() {
        foreach (InventorySlot invSlot in inventoryUI.slots) {
            if (invSlot.isEmpty) return invSlot;
        }
        return null;
    }
    public List<InventorySlot> GetEmptySlots() {
        List<InventorySlot> slots = new();
        foreach (InventorySlot slot in inventoryUI.slots) {
            if (slot.isEmpty) slots.Add(slot);
        }
        return slots;
    }


    public bool HaveEmptySLot() {
        throw new NotImplementedException();
    }
    public Item FindItem(Item item) {
        foreach (Item it in itemList) {
            if (it == item) return it;
        }
        return null;
    }
    public List<Item> GetItemList() {
        return itemList;
    }
}

