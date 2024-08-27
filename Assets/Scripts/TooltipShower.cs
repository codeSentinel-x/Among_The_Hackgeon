using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipShower : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        TooltipManager._I.ShowTooltip(TooltipType.Weapon, AssetManager.LoadWeaponByName("Sniper"), transform.position);
    }
    void OnTriggerExit2D(Collider2D other) {
        TooltipManager._I.HideTooltip(TooltipType.Weapon);
    }
}
