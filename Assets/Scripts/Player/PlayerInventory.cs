using MyUtils.Classes;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public static PlayerInventory _I;
    public Inventory _inventory;
    public Inventory_UI inventory_UI;
    public int inventorySize;
    [SerializeField] GameObject canvasG;
    private bool isCanvasActive;
    private void Start() {
        _I = this;
        _inventory = new(inventory_UI, inventorySize);
        inventory_UI.SetInventory(_inventory);
        inventory_UI._onEquipmentChange += RefreshStat;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (CommandConsole.I.commandInput.isFocused) return;
            isCanvasActive = !isCanvasActive;
            if (!isCanvasActive) Inventory_UI._onInventoryClose?.Invoke();
            canvasG.SetActive(isCanvasActive);
        }
    }


    private void RefreshStat() {
        //TODO refresh inventory 
        throw new System.NotImplementedException();
    }
}
