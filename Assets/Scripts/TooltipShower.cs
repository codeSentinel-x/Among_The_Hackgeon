using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipShower : MonoBehaviour {
    public void Setup(WeaponSO source) {
        _source = source;
    }
    public WeaponSO _source;
    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        TooltipManager._I.ShowTooltip(TooltipType.Weapon, _source);
    }
    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;
        TooltipManager._I.HideTooltip(TooltipType.Weapon);
    }
}
