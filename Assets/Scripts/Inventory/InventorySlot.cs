using MyUtils.Classes;
using MyUtils.Functions;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Mouse_DraggableItemFromInventory _mouseHandler;
    public Inventory _inventory;
    public DraggableSlot _itemSlotPF;
    public Item _item;
    public DraggableSlot _dSlot;
    public bool _isEmpty;
    public bool _isFull;

    public void Setup(Inventory inv) {
        _inventory = inv;
        _isEmpty = true;
        _isFull = false;
    }
    public void AddItem(Item it) {
        if (_dSlot != null) Destroy(_dSlot.gameObject);
        _dSlot = Instantiate(_itemSlotPF, this.transform).GetComponent<DraggableSlot>();
        _item = it;
        _dSlot.Setup(_item, this);
        _isEmpty = false;
        _isFull = (_item._amount == _item._maxAmount);

    }
    public void RemoveItem() {
        Destroy(_dSlot.gameObject);
        _item = null;
        _isEmpty = true;
        _isFull = false;
    }

    public void RefreshItemAmount(int amount) {
        if (amount > _item._maxAmount) amount = _item._maxAmount;
        _item._amount = amount;
        _isFull = amount == _item._maxAmount;
        _dSlot.UpdateAmount();
    }
    public void OnPointerClick(PointerEventData eventData) {
        switch (eventData.button) {
            case PointerEventData.InputButton.Left: {
                    OnLeftClick();
                    _inventory.RefreshItemList();
                    break;
                }
            case PointerEventData.InputButton.Right: {
                    OnRightClick();
                    _inventory.RefreshItemList();

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
        if (Mouse_DraggableItemFromInventory._I == null) Instantiate(_mouseHandler, transform.parent.parent);
        Mouse_DraggableItemFromInventory mouseHandlerI = Mouse_DraggableItemFromInventory._I;


        if (mouseHandlerI._hasItem) {
            if (_isEmpty) {
                AddItem(mouseHandlerI._dSlot._currentItem);
                mouseHandlerI.Reset();
            } else {
                if (_item._itemBase == mouseHandlerI._dSlot._currentItem._itemBase && !_isFull) {
                    int control = _item._maxAmount - _item._amount;
                    if (mouseHandlerI._dSlot._currentItem._amount > control) {
                        mouseHandlerI._dSlot.UpdateAmount(mouseHandlerI._dSlot._currentItem._amount - control);
                        RefreshItemAmount(_item._maxAmount);


                    } else {
                        RefreshItemAmount(_item._amount + mouseHandlerI._dSlot._currentItem._amount);
                        Destroy(mouseHandlerI.gameObject);


                    }
                } else {
                    mouseHandlerI.ChangeItem(_item, out Item newItem);
                    AddItem(newItem);


                }
            }
        } else {
            if (_isEmpty) {
                //Do nothing;
            } else {
                mouseHandlerI.SetUp(_item);
                RemoveItem();

            }
        }
    }
    public void OnRightClick() {
        if (Mouse_DraggableItemFromInventory._I == null) Instantiate(_mouseHandler, transform.parent.parent);
        Mouse_DraggableItemFromInventory mouseHandlerI = Mouse_DraggableItemFromInventory._I;
        if (!_isEmpty && _item._amount > 1 && !mouseHandlerI._hasItem) {
            InventoryF.Split(_item._amount, out int value1, out int value2);
            RefreshItemAmount(value1);
            mouseHandlerI.SetUp(_item, value2);
        } else if (mouseHandlerI._hasItem) {
            if (_isEmpty) {
                int amount = mouseHandlerI._dSlot._currentItem._amount;
                if (amount == 1) {
                    AddItem(mouseHandlerI._dSlot._currentItem);
                    mouseHandlerI.Reset();
                } else {
                    AddItem(mouseHandlerI._dSlot._currentItem);
                    mouseHandlerI.Reset();
                    mouseHandlerI = Instantiate(_mouseHandler, transform.parent.parent);
                    mouseHandlerI.SetUp(_item, amount - 1);
                    RefreshItemAmount(1);
                }
            } else {
                if (_isFull) return;
                if (_item._itemBase == mouseHandlerI._dSlot._currentItem._itemBase) {
                    int amount = mouseHandlerI._dSlot._currentItem._amount;
                    if (amount == 1) {
                        RefreshItemAmount(_item._amount + 1);
                        mouseHandlerI.Reset();
                    } else {
                        mouseHandlerI.Reset();
                        mouseHandlerI = Instantiate(_mouseHandler, transform.parent.parent);
                        mouseHandlerI.SetUp(_item, amount - 1);
                        RefreshItemAmount(_item._amount + 1);
                    }
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (Mouse_DraggableItemFromInventory._I != null) if (Mouse_DraggableItemFromInventory._I._hasItem) return;
        if (!_isEmpty) {
            ItemTooltip.Show(_item);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        ItemTooltip.Hide();
    }
}
