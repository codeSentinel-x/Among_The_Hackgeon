using System;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour {
    public TextMeshProUGUI _nameText;
    public TextMeshProUGUI _shortDescText;
    public TextMeshProUGUI _longDescText;

    public void Setup<T>(T source){
        if(typeof(T) ==  typeof(WeaponSO)){

        }
    }
}
