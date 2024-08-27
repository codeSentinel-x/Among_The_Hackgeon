using UnityEngine;

public class TooltipManager : MonoBehaviour {
    public GameObject _tooltipCanvas;
    public GameObject _WeaponTooltip;
    public GameObject _specialIemTooltip;
    public GameObject _itemTooltip;

    void Awake() {
        DontDestroyOnLoad(_tooltipCanvas);
    }
    public void ShowTooltip(TooltipType type){
        GetTooltip(type).SetActive(true);
        
    }
    public void HideTooltip(TooltipType type){
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