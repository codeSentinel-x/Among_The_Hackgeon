using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialInventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Image itemImagePF;
    public Mouse_DraggableItemFromInventory mouseHandler;
    public Inventory inventory;
    public DraggableSlot itemSlotPF;
    public Item item;
    public DraggableSlot dSlot;

    public bool isEmpty;
    public bool isFull;

    [Tooltip("0 - headSlot \n 1 - chest slot \n 2 - leg slot \n 3 - bag slot \n 4 - belt slot \n 5 - rune slot \n 6 - special book slot \n 7 - food slot \n  8 - health potion slot \n 9 - mana potion slot")]
    public int specialSlotIndex;

    public bool CanBeAdded(Item item) {
        if (!item.itemBase.isEquipable) return false;
        if (item.equipableItemBase.specialSlotIndex == specialSlotIndex) return true;
        else return false;
    }
    public void Setup(Inventory inv) {
        inventory = inv;
        isEmpty = true;
        isFull = false;
    }
    public void AddItem(Item it) {
        if (!CanBeAdded(it)) return;
        if (dSlot != null) Destroy(dSlot.gameObject);
        dSlot = Instantiate(itemSlotPF, this.transform).GetComponent<DraggableSlot>();
        item = it;
        dSlot.Setup(item, this);
        isEmpty = false;
        isFull = (item.amount == item.maxStack);
        Player_Stats.OnInventoryEquipmentChange?.Invoke(null, item);


    }
    public void RemoveItem() {
        Player_Stats.OnInventoryEquipmentChange?.Invoke(item, null);
        Destroy(dSlot.gameObject);
        item = null;
        isEmpty = true;
        isFull = false;
    }

    public void RefreshItemAmount(int amount) {
        if (amount > item.maxStack) amount = item.maxStack;
        item.amount = amount;
        isFull = amount == item.maxStack;
        dSlot.UpdateAmount();
    }
    public void OnPointerClick(PointerEventData eventData) {
        switch (eventData.button) {
            case PointerEventData.InputButton.Left: {
                    OnLeftClick();
                    break;
                }
            case PointerEventData.InputButton.Right: {
                    OnRightClick();

                    break;
                }
            case PointerEventData.InputButton.Middle: {
                    //TODO Item custom amount picker
                    break;
                }
            default: break;
        }
    }
    public void OnLeftClick() {
        if (Mouse_DraggableItemFromInventory.I == null) Instantiate(mouseHandler, transform.parent.parent);
        Mouse_DraggableItemFromInventory mouseHandlerI = Mouse_DraggableItemFromInventory.I;


        if (mouseHandlerI.hasItem) {
            if (!CanBeAdded(mouseHandlerI.dSlot.currentItem)) return;
            if (isEmpty) {
                AddItem(mouseHandlerI.dSlot.currentItem);
                mouseHandlerI.Reset();
            }
            else {
                if (item.itemBase == mouseHandlerI.dSlot.currentItem.itemBase && !isFull) {
                    int control = item.maxStack - item.amount;
                    if (mouseHandlerI.dSlot.currentItem.amount > control) {
                        mouseHandlerI.dSlot.UpdateAmount(mouseHandlerI.dSlot.currentItem.amount - control);
                        RefreshItemAmount(item.maxStack);


                    }
                    else {
                        RefreshItemAmount(item.amount + mouseHandlerI.dSlot.currentItem.amount);
                        Destroy(mouseHandlerI.gameObject);


                    }
                }
                else {
                    mouseHandlerI.ChangeItem(item, out Item newItem);
                    RemoveItem();
                    AddItem(newItem);


                }
            }
        }
        else {
            if (isEmpty) {
                //Do nothing;
            }
            else {
                mouseHandlerI.SetUp(item);
                RemoveItem();

            }
        }
    }
    public void OnRightClick() {
        if (Mouse_DraggableItemFromInventory.I == null) Instantiate(mouseHandler, transform.parent.parent);
        Mouse_DraggableItemFromInventory mouseHandlerI = Mouse_DraggableItemFromInventory.I;
        if (!isEmpty && item.amount > 1 && !mouseHandlerI.hasItem) {
            Special.MyFunctions.Split(item.amount, out int value1, out int value2);
            RefreshItemAmount(value1);
            mouseHandlerI.SetUp(item, value2);
        }
        else if (mouseHandlerI.hasItem) {
            if (!CanBeAdded(mouseHandlerI.dSlot.currentItem)) return;

            if (isEmpty) {
                int amount = mouseHandlerI.dSlot.currentItem.amount;
                if (amount == 1) {
                    AddItem(mouseHandlerI.dSlot.currentItem);
                    mouseHandlerI.Reset();
                }
                else {
                    AddItem(mouseHandlerI.dSlot.currentItem);
                    mouseHandlerI.Reset();
                    mouseHandlerI = Instantiate(mouseHandler, transform.parent.parent);
                    mouseHandlerI.SetUp(item, amount - 1);
                    RefreshItemAmount(1);
                }
            }
            else {
                if (isFull) return;
                if (item.itemBase == mouseHandlerI.dSlot.currentItem.itemBase) {
                    int amount = mouseHandlerI.dSlot.currentItem.amount;
                    if (amount == 1) {
                        RefreshItemAmount(item.amount + 1);
                        mouseHandlerI.Reset();
                    }
                    else {
                        mouseHandlerI.Reset();
                        mouseHandlerI = Instantiate(mouseHandler, transform.parent.parent);
                        mouseHandlerI.SetUp(item, amount - 1);
                        RefreshItemAmount(item.amount + 1);
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Mouse_DraggableItemFromInventory.I != null) if (Mouse_DraggableItemFromInventory.I.hasItem) return;
        if (!isEmpty) {
            ItemTooltip.Show(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        ItemTooltip.Hide();
    }
}

