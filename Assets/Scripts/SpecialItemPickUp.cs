using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.Classes;
using MyUtils.Structs;
using UnityEngine;

public class SpecialItemPickUp : MonoBehaviour {

    public string _name;
    public SpecialItemSO _defaultSetting;
    void Awake() {
        _defaultSetting = GameDataManager.LoadItemByName(_name);
        GetComponent<SpriteRenderer>().sprite = _defaultSetting._sprite;
    }
    public void Apply() {
        foreach (var s in _defaultSetting._statsToChange) {

            var f = PlayerController._I._data.GetType().GetField(s._name);
            var p = f.GetValue(PlayerController._I._data) as PlayerStat;
            var v = p.GetValue();
            if (s._modifier != 0) p.AddModifier(s._modifier);
            if (s._multiplier != 0) p.AddMultiplier(s._multiplier);
            Debug.Log($"value before change: {v}, new value{p.GetValue()}");
        }
    }
    public void Clear() {
        foreach (var s in _defaultSetting._statsToChange) {

            var f = PlayerController._I._data.GetType().GetField(s._name);
            var p = f.GetValue(PlayerController._I._data) as PlayerStat;
            var v = p.GetValue();
            if (s._modifier != 0) p.RemoveMultiplier(s._modifier);
            if (s._multiplier != 0) p.RemoveModifier(s._multiplier);
            Debug.Log($"value before change: {v}, new value{p.GetValue()}");
        }
    }

}


