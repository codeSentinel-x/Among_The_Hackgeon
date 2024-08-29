using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class InputManager : MonoBehaviour {
    public static InputManager _I;
    public KeyArrayElement[] _keyDownArray;
    public KeyArrayElement[] _keyUpArray;
    public Dictionary<KeyBindType, KeyBind> _keyDownBinds;
    public Dictionary<KeyBindType, KeyBind> _keyUpBinds;
    void Awake() {
        _I = this;
        _keyDownBinds = new();
        _keyUpBinds = new();
        foreach (var k in _keyUpArray) _keyUpBinds.Add(k._type, k._keyBind);
        foreach (var k in _keyDownArray) _keyDownBinds.Add(k._type, k._keyBind);

    }

    void Update() {
        foreach (var k in _keyDownBinds) {
            if (k.Value._onKeyAction == null) continue;
            if (Input.GetKeyDown(k.Value._key)) k.Value._onKeyAction.Invoke();
        }
        foreach (var k in _keyUpBinds) {
            if (k.Value._onKeyAction == null) continue;
            if (Input.GetKeyUp(k.Value._key)) k.Value._onKeyAction.Invoke();
        }
    }
    public UnityEvent GetKeyPressed(KeyBindType type) {
        _keyDownBinds.TryGetValue(type, out var key);
        return key._onKeyAction;
    }
    public void ChangeKeyBind(KeyBindType type, KeyCode newBind, int mode = 0) {
        switch (mode) {
            case 0: {
                    if (_keyDownBinds.TryGetValue(type, out var k)) k._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyDownBinds");
                    break; 
                }
            case 1: {
                    if (_keyUpBinds.TryGetValue(type, out var k)) k._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyUpBinds");
                    break;
                }
            case 2: {
                    if (_keyDownBinds.TryGetValue(type, out var kD)) kD._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyDownBinds");
                    if (_keyUpBinds.TryGetValue(type, out var kU)) kU._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyUpBinds");
                    break;
                }
            default: break;
        }
    }
}
[Serializable]
public struct KeyArrayElement {
    public KeyBindType _type;
    public KeyBind _keyBind;
}
[Serializable]
public class KeyBind {
    public KeyCode _key;
    public UnityEvent _onKeyAction;
}
public enum KeyBindType {
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
    UseBlank,
    Dash,
    ShowHelp,
    ShowReadme,
    Pause,
    Interact,
    Restart,
    ExitToStartScreen,
    Exit,
}