using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    public Image _image;
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _shortDescText;
    public TextMeshProUGUI _longDescText;

    public void Setup<T>(T source) where T : ScriptableObject {
        switch (source) {
            case WeaponSO w: {
                    _image.sprite = w._sprite;
                    _nameText.text = w._name;
                    _shortDescText.text = w._shortDesc;
                    _longDescText.text = $"D:{w._bulletSetting._damage} S:{w._spread} A:{w._maxBullet} R:{w._reloadTime} D:{w._shotDelay}";
                    break;
                }
            case SpecialItemSO s: {
                    _image.sprite = s._sprite;
                    _nameText.text = s._name;
                    _shortDescText.text = s._shortDesc;
                    _longDescText.text = "|_null_|";
                    break;
                }
        }
    }
}
