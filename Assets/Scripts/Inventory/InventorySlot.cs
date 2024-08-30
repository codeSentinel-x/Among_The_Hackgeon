using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Mouse_DraggableItemFromInventory mouseHandler;
    public Inventory inventory;
    public DraggableSlot itemSlotPF;
    public Item item;
    public DraggableSlot dSlot;

    public bool isEmpty;
    public bool isFull;

    public void Setup(Inventory inv) {
        inventory = inv;
        isEmpty = true;
        isFull = false;
    }
    public void AddItem(Item it) {
        if (dSlot != null) Destroy(dSlot.gameObject);
        dSlot = Instantiate(itemSlotPF, this.transform).GetComponent<DraggableSlot>();
        item = it;
        dSlot.Setup(item, this);
        isEmpty = false;
        isFull = (item.amount == item.maxStack);

    }
    public void RemoveItem() {
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
                    inventory.RefreshItemList();
                    break;
                }
            case PointerEventData.InputButton.Right: {
                    OnRightClick();
                    inventory.RefreshItemList();

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
//              For SEBASTIAN


// public static class ConsumableItemFunctions{

// 	public enum ItemType{
// 		healthPotion,
// 		manaPotion,
// 		staminaPotion
// 	}
//     public static void UseItem(ItemType type)
//     {
//         switch (type) { 

//             case ItemType.healthPotion : {
//                 UseHealthPotion();
//                 break;
//             }
//             case ItemType.manaPotion : {
//                 UseManaPotion();
//                 break;
//             }
//             case ItemType.staminaPotion : {
//                 UseStaminaPotion();
//                 break;
//             }
//             default : break;

//         }

//     }
// 	public static void UseHealthPotion(){
// 		//code
// 	}
// 	public static void UseManaPotion(){
// 		//code
// 	}
// 	public static void UseStaminaPotion(){
// 		//code
// 	}	
// }
// class Example
// {
//     int x;
//     int twiceX;
//     public int Sqrt(int v)
//     {
//         return v * v;
//     }
//     public void Sqrtt(int v, out int value)
//     {
//         value = v * v;
//     }
//     public void Sqrtt(int v, out int value, out int twiceValue)
//     {
//         value = v * v;
//         twiceValue = value * 2;
//     }

//     void Test()
//     {
//         x = Sqrt(x);
//         Sqrtt(x, out x);
//         Sqrtt(x, out x, out twiceX);
//     }
// }
