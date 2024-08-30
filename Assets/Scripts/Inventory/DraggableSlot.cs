using MyUtils.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraggableSlot : MonoBehaviour {
    public InventorySlot _parentSlot;
    public SpecialInventorySlot _parentSpecialSlot;
    public TextMeshProUGUI _amountTXT;
    public Image _itemImage;
    public Item _currentItem;


    public void Setup(Item item, InventorySlot slot) {
        _parentSlot = slot;
        _currentItem = Item.CreateNewItemFromItem(item);
        _itemImage.sprite = item._itemBase._sprite;
        if (item._amount > 1) {
            _amountTXT.enabled = true;
            _amountTXT.text = item._amount.ToString();
        }
    }
    public void Setup(Item item, SpecialInventorySlot slot) {
        _parentSpecialSlot = slot;
        _currentItem = Item.CreateNewItemFromItem(item);
        _itemImage.sprite = item._itemBase._sprite;
        if (item._amount > 1) {
            _amountTXT.enabled = true;
            _amountTXT.text = item._amount.ToString();
        }
    }
    public void Setup(Item item) {
        _currentItem = Item.CreateNewItemFromItem(item);
        _itemImage.sprite = item._itemBase._sprite;
        if (item._amount > 1) {
            _amountTXT.enabled = true;
            _amountTXT.text = item._amount.ToString();
        }
    }
    public void Setup(Item item, int amount) {
        _currentItem = Item.CreateNewItemFromItem(item);
        _currentItem._amount = amount;
        _itemImage.sprite = item._itemBase._sprite;
        if (_currentItem._amount > 1) {
            _amountTXT.enabled = true;
            _amountTXT.text = _currentItem._amount.ToString();
        }
    }
    public void RefreshTXT() {
        _amountTXT.enabled = _currentItem._amount > 1;
        _amountTXT.text = _currentItem._amount.ToString();
    }
    public void UpdateAmount() {
        if (_parentSpecialSlot != null) _currentItem = _parentSpecialSlot._item;
        if (_parentSlot != null) _currentItem = _parentSlot._item;
        _amountTXT.enabled = _currentItem._amount > 1;
        _amountTXT.text = _currentItem._amount.ToString();
    }
    public void UpdateAmount(int newAmount) {
        _currentItem._amount = newAmount;
        _amountTXT.enabled = _currentItem._amount > 1;
        _amountTXT.text = _currentItem._amount.ToString();
    }

}

