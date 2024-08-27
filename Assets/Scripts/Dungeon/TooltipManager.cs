using UnityEngine;

public class TooltipManager : MonoBehaviour {
    public static TooltipManager _I;
    public GameObject _tooltipCanvas;
    public GameObject _WeaponTooltip;
    public GameObject _specialIemTooltip;
    public GameObject _itemTooltip;

    void Awake() {
        _I = this;
        DontDestroyOnLoad(_tooltipCanvas);
    }
    public void ShowTooltip<T>(TooltipType type, T source, Vector3 pos) where T : ScriptableObject {
        var v = GetTooltip(type);
        v.SetActive(true);
        v.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(pos);
        v.GetComponent<Tooltip>().Setup(source);
    }
    public void HideTooltip(TooltipType type) {
        GetTooltip(type).SetActive(false);
    }
    public GameObject GetTooltip(TooltipType type) {
        return type switch {
            TooltipType.Weapon => _WeaponTooltip,
            TooltipType.SpecialItem => _specialIemTooltip,
            TooltipType.Item => _itemTooltip,
            _ => null,
        };
    }
}
public enum TooltipType {
    Weapon,
    SpecialItem,
    Item,

}