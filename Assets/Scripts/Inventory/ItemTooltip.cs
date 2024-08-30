using MyUtils.Classes;
using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour {
    private static LTDescr _delay;
    public static ItemTooltip _I;
    public TextMeshProUGUI _nameTXT;
    public TextMeshProUGUI _shortDescriptionTXT;
    public TextMeshProUGUI _statsTXT;
    public float _offset;
    public GameObject _canvasG;
    public bool _isCanvasActive = true;

    private void Awake() {
        _I = this;
        Hide();
    }
    private void Start() {
        Inventory_UI._onInventoryClose += Hide;
    }
    private void Update() {

        Vector2 position = Input.mousePosition;
        transform.position = new Vector2(position.x + _offset, position.y);
    }

    public static void Show(string name) {
        _delay = LeanTween.delayedCall(1f, () => {
            Vector2 position = Input.mousePosition;
            _I.transform.position = new Vector2(position.x + _I._offset, position.y);
            _I._nameTXT.text = name;
            _I._nameTXT.gameObject.SetActive(true);
            _I.gameObject.SetActive(true);
        });
    }
    public static void Show(string name, string shortDescription) {
        _I._shortDescriptionTXT.gameObject.SetActive(true);
        _I._shortDescriptionTXT.text = shortDescription;
        Show(name);
    }
    public static void Show(string name, string shortDescription, string stats) {
        _I._statsTXT.gameObject.SetActive(true);
        _I._statsTXT.text = stats;
        Show(name, shortDescription);
    }
    public static void Show(Item item) {
        string name = item._itemBase._name;
        string shortDescription = item._itemBase._shortDesc;
        // if (item._itemType != Item.ItemType.Equipable) {
        Show(name, shortDescription);
        // }
        // else {
        // string stats = item.GetEquipableItemStatsInString();
        // Show(name, shortDescription, stats);
        // }
    }


    public static void Hide() {
        _I._nameTXT.gameObject.SetActive(false);
        _I._shortDescriptionTXT.gameObject.SetActive(false);
        _I._statsTXT.gameObject.SetActive(false);
        _I.gameObject.SetActive(false);
        if (_delay != null) LeanTween.cancel(_delay.uniqueId);
    }

}
