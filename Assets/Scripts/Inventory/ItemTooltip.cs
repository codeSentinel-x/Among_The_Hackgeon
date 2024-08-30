using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemTooltip : MonoBehaviour {
    private static LTDescr delay;
    public static ItemTooltip I;
    public TextMeshProUGUI nameTXT;
    public TextMeshProUGUI shortDescriptionTXT;
    public TextMeshProUGUI statsTXT;
    public float offset;
    public GameObject canvasG;
    public bool isCanvasActive = true;

    private void Awake() {
        I = this;
        Hide();
    }
    private void Start() {
        Inventory_UI.onInventoryClose += Hide;
    }
    private void Update() {

        Vector2 position = Input.mousePosition;
        transform.position = new Vector2(position.x + offset, position.y);
    }

    public static void Show(string name) {
        delay = LeanTween.delayedCall(1f, () => {
            Vector2 position = Input.mousePosition;
            I.transform.position = new Vector2(position.x + I.offset, position.y);
            I.nameTXT.text = name;
            I.nameTXT.gameObject.SetActive(true);
            I.gameObject.SetActive(true);
        });
    }
    public static void Show(string name, string shortDescription) {
        I.shortDescriptionTXT.gameObject.SetActive(true);
        I.shortDescriptionTXT.text = shortDescription;
        Show(name);
    }
    public static void Show(string name, string shortDescription, string stats) {
        I.statsTXT.gameObject.SetActive(true);
        I.statsTXT.text = stats;
        Show(name, shortDescription);
    }
    public static void Show(Item item) {
        string name = item.itemBase.itemName;
        string shortDescription = item.itemBase.itemShortDescription;
        if (item.itemType != Item.ItemType.Equipable) {
            Show(name, shortDescription);
        }
        else {
            string stats = item.GetEquipableItemStatsInString();
            Show(name, shortDescription, stats);
        }
    }


    public static void Hide() {
        I.nameTXT.gameObject.SetActive(false);
        I.shortDescriptionTXT.gameObject.SetActive(false);
        I.statsTXT.gameObject.SetActive(false);
        I.gameObject.SetActive(false);
        if (delay != null) LeanTween.cancel(delay.uniqueId);
    }

}
