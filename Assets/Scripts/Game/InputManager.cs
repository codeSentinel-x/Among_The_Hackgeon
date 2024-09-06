using System;
using MyUtils.Classes;
using MyUtils.Enums;
using MyUtils.SaveSystem;
using UnityEngine;
public class InputManager : MonoBehaviour {
    public const string KEY_BIND_NAME = "KeyBinds";
    public static Action _onKeyBindChange;
    public static InputManager _I;
    public KeyBindData _keyBindData;
    public KeyBindArrayElement[] _keyBinds;
    void Awake() {
        if (_I != null) Destroy(this.gameObject);
        else _I = this;
        // SaveKeyBinds();
        LoadDefaultSetting();
        LoadKeyBinds();

    }
    // }
    void OnApplicationQuit() {
        SaveKeyBinds();
    }
    public void LoadDefaultSetting() {


        _keyBindData = new();
        _keyBindData._keyBinds ??= new();
        _keyBindData._keyBinds = new();
        _keyBindData._keyBindsAr = new KeyBindArrayElement[_keyBinds.Length];

        for (int i = 0; i < _keyBinds.Length; i++) {
            var k = _keyBinds[i];
            _keyBindData._keyBinds.Add(k._type, k._keyBind);
            _keyBindData._keyBindsAr[i] = k;
            // Debug.Log("added");
        }
        SaveKeyBinds();
    }
    public void LoadKeyBinds() {
        _keyBindData = SaveSystem.Load<KeyBindData>(SaveSystem.KEY_BIND_DATA_SAVE_PATH, KEY_BIND_NAME);
        for (int i = 0; i < _keyBindData._keyBindsAr.Length; i++) {
            var k = _keyBindData._keyBindsAr[i];
            _keyBindData._keyBinds.Add(k._type, k._keyBind);
        }

    }
    public KeyCode GetKey(KeyBindType type) {
        if (_keyBindData._keyBinds.TryGetValue(type, out var k)) {
            // Debug.Log($"Key successfully returned. KeyType: {type}");
            return k._key;
        } else {
            Debug.Log("No value for given key");
            return default;
        }
    }
    public void SaveKeyBinds() {
        SaveSystem.Save(SaveSystem.KEY_BIND_DATA_SAVE_PATH, KEY_BIND_NAME, _keyBindData);
    }
}
