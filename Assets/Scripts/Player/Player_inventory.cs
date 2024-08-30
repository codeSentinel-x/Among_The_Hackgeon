using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_inventory : MonoBehaviour {
    public static Player_inventory I;
    public Inventory inventory;
    public Inventory_UI inventory_UI;
    public int inventorySize;
    public ItemBaseSO testItem;
    public EquipableItemBase secondTestItem;
    public EquipableItemBase thirdTestItem;

    [SerializeField] GameObject canvasG;
    private bool isCanvasActive;
    private void Start() {
        I = this;
        inventory = new(inventory_UI, inventorySize);
        inventory_UI.SetInventory(inventory);
        inventory_UI.onEquipmentChange += RefreshStat;
        Test();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (CommandConsole.I.commandInput.isFocused) return;
            isCanvasActive = !isCanvasActive;
            if (!isCanvasActive) Inventory_UI.onInventoryClose?.Invoke();
            canvasG.SetActive(isCanvasActive);
        }
    }

    private void Test() {
        Item item = new(testItem, 1);
        inventory.AddItem(item);
        inventory.AddItem(item);
        inventory.AddItem(item);
        Item item2 = new(secondTestItem, 1);
        inventory.AddItem(item2);
        Item item3 = new(thirdTestItem, 1);
        inventory.AddItem(item3);
    }

    private void RefreshStat() {
        //TODO refresh inventory 
        throw new System.NotImplementedException();
    }
}
