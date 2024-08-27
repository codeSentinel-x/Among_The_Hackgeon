using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipShower : MonoBehaviour {

    public void Setup(WeaponSO source) {
        _wSource = source;
    }
    public void Setup(SpecialItemSO source) {
        _sSource = source;
    }

    public WeaponSO _wSource;
    public SpecialItemSO _sSource;

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        if (_wSource != null) TooltipManager._I.ShowTooltip(TooltipType.Weapon, _wSource);
        else if (_sSource != null) TooltipManager._I.ShowTooltip(TooltipType.SpecialItem, _wSource);
    }
    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        if (_wSource != null) TooltipManager._I.HideTooltip(TooltipType.Weapon);
        else if (_sSource != null) TooltipManager._I.HideTooltip(TooltipType.SpecialItem);
    }
}
