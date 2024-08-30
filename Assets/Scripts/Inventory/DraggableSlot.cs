using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableSlot : MonoBehaviour {
    public InventorySlot parentSlot;
    public SpecialInventorySlot parentSpecialSlot;
    public TextMeshProUGUI amountTXT;
    public Image itemImage;
    public Item currentItem;


    public void Setup(Item item, InventorySlot slot) {
        parentSlot = slot;
        currentItem = Item.CreateNewItemFromItem(item);
        itemImage.sprite = item.itemSprite;
        if (item.amount > 1) {
            amountTXT.enabled = true;
            amountTXT.text = item.amount.ToString();
        }
    }
    public void Setup(Item item, SpecialInventorySlot slot) {
        parentSpecialSlot = slot;
        currentItem = Item.CreateNewItemFromItem(item);
        itemImage.sprite = item.itemSprite;
        if (item.amount > 1) {
            amountTXT.enabled = true;
            amountTXT.text = item.amount.ToString();
        }
    }
    public void Setup(Item item) {
        currentItem = Item.CreateNewItemFromItem(item);
        itemImage.sprite = item.itemSprite;
        if (item.amount > 1) {
            amountTXT.enabled = true;
            amountTXT.text = item.amount.ToString();
        }
    }
    public void Setup(Item item, int amount) {
        currentItem = Item.CreateNewItemFromItem(item);
        currentItem.amount = amount;
        itemImage.sprite = item.itemSprite;
        if (currentItem.amount > 1) {
            amountTXT.enabled = true;
            amountTXT.text = currentItem.amount.ToString();
        }
    }
    public void RefreshTXT() {
        amountTXT.enabled = currentItem.amount > 1;
        amountTXT.text = currentItem.amount.ToString();
    }
    public void UpdateAmount() {
        if (parentSpecialSlot != null) currentItem = parentSpecialSlot.item;
        if (parentSlot != null) currentItem = parentSlot.item;
        amountTXT.enabled = currentItem.amount > 1;
        amountTXT.text = currentItem.amount.ToString();
    }
    public void UpdateAmount(int newAmount) {
        currentItem.amount = newAmount;
        amountTXT.enabled = currentItem.amount > 1;
        amountTXT.text = currentItem.amount.ToString();
    }

}

