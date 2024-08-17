using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.Classes;
using UnityEngine;

public class SpecialItemPickUp : MonoBehaviour {
    public StatChangeObject[] statsToChange;
        void Start() {
        statsToChange = new StatChangeObject[1];
        statsToChange[0] = new StatChangeObject() { _name = "_movementSpeed" };
        Apply();
    }
    public void Apply() {
        var v = PlayerController._I._data.GetType().GetField(statsToChange[0]._name).GetValue(typeof(PlayerStat)) as PlayerStat;
        Debug.Log(v.GetValue());
    }

}

[Serializable]
public struct StatChangeObject {
    public string _name;
    public float _multiplier;
    public float _a_modifier;
}
