using System;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour {
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _shortDescText;
    public TextMeshProUGUI _longDescText;

    public void Setup<T>(T source) where T : ScriptableObject {
        switch (source) {
            case WeaponSO w: {
                    _nameText.text = w._name;
                    _shortDescText.text = w._shortDesc;
                    _longDescText.text = $"Damage: {w._bulletSetting._damage}\nSpread: {w._spread}{Convert.ToChar(248)}\nAmmo: {w._maxBullet}\nReload time: {w._reloadTime}\nShoot delay:{w._shotDelay}\nIs automatic: {w._auto}";
                    break;
                }
            case SpecialItemSO s: {
                    break;
                }
        }
    }
}
