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
    public UnityEvent GetKeyPressed(KeyBindType type, KeyPressMode mode) {
        switch (mode) {
            case KeyPressMode.KeyDown: {
                    _keyDownBinds.TryGetValue(type, out var key);
                    return key._onKeyAction;
                }
            case KeyPressMode.KeyUP: {
                    _keyUpBinds.TryGetValue(type, out var key);
                    return key._onKeyAction;
                }
            default: return null;
        }
    }
    public void ChangeKeyBind(KeyBindType type, KeyCode newBind, KeyPressMode mode) {
        switch (mode) {
            case KeyPressMode.KeyDown: {
                    if (_keyDownBinds.TryGetValue(type, out var k)) k._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyDownBinds");
                    break;
                }
            case KeyPressMode.KeyUP: {
                    if (_keyUpBinds.TryGetValue(type, out var k)) k._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyUpBinds");
                    break;
                }
            case KeyPressMode.Both: {
                    if (_keyDownBinds.TryGetValue(type, out var kD)) kD._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyDownBinds");
                    if (_keyUpBinds.TryGetValue(type, out var kU)) kU._key = newBind;
                    else Debug.Log($"No value with given key: {type} in _keyUpBinds");
                    break;
                }
            default: break;
        }
    }
    public void InitializeDefaultKeyBinds() {
        if (_keyDownBinds.TryGetValue(KeyBindType.MoveLeft, out var mLD)) {
            mLD._key = KeyCode.A;
        } else {
            _keyDownBinds.Add(KeyBindType.MoveLeft, new KeyBind() { _key = KeyCode.A });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.MoveRight, out var mRD)) {
            mRD._key = KeyCode.R;
        } else {
            _keyDownBinds.Add(KeyBindType.MoveRight, new KeyBind() { _key = KeyCode.R });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.MoveUp, out var mUD)) {
            mUD._key = KeyCode.W;
        } else {
            _keyDownBinds.Add(KeyBindType.MoveUp, new KeyBind() { _key = KeyCode.W });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.MoveDown, out var mDD)) {
            mDD._key = KeyCode.S;
        } else {
            _keyDownBinds.Add(KeyBindType.MoveDown, new KeyBind() { _key = KeyCode.S });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Dash, out var dD)) {
            dD._key = KeyCode.Mouse1;
        } else {
            _keyDownBinds.Add(KeyBindType.Dash, new KeyBind() { _key = KeyCode.Mouse1 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.ShowHelp, out var sHD)) {
            sHD._key = KeyCode.F1;
        } else {
            _keyDownBinds.Add(KeyBindType.ShowHelp, new KeyBind() { _key = KeyCode.F1 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.ShowReadme, out var sRD)) {
            sRD._key = KeyCode.F6;
        } else {
            _keyDownBinds.Add(KeyBindType.ShowReadme, new KeyBind() { _key = KeyCode.F6 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Interact, out var iD)) {
            iD._key = KeyCode.E;
        } else {
            _keyDownBinds.Add(KeyBindType.Interact, new KeyBind() { _key = KeyCode.E });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Pause, out var pD)) {
            pD._key = KeyCode.F2;
        } else {
            _keyDownBinds.Add(KeyBindType.Pause, new KeyBind() { _key = KeyCode.F2 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.ExitToStartScreen, out var eTSD)) {
            eTSD._key = KeyCode.F3;
        } else {
            _keyDownBinds.Add(KeyBindType.ExitToStartScreen, new KeyBind() { _key = KeyCode.F3 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Exit, out var eD)) {
            eD._key = KeyCode.F4;
        } else {
            _keyDownBinds.Add(KeyBindType.Exit, new KeyBind() { _key = KeyCode.F4 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Shoot, out var sD)) {
            sD._key = KeyCode.Mouse1;
        } else {
            _keyDownBinds.Add(KeyBindType.Shoot, new KeyBind() { _key = KeyCode.Mouse1 });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Reload, out var rD)) {
            rD._key = KeyCode.R;
        } else {
            _keyDownBinds.Add(KeyBindType.Reload, new KeyBind() { _key = KeyCode.R });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.ChangeWeapon, out var cWD)) {
            cWD._key = KeyCode.LeftShift;
        } else {
            _keyDownBinds.Add(KeyBindType.ChangeWeapon, new KeyBind() { _key = KeyCode.LeftShift });
        }
        if (_keyDownBinds.TryGetValue(KeyBindType.Loop, out var lD)) {
            lD._key = KeyCode.F5;
        } else {
            _keyDownBinds.Add(KeyBindType.Loop, new KeyBind() { _key = KeyCode.F5 });
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
    Shoot,
    Reload,
    ChangeWeapon,
    Loop,
    ShowHelp,
    ShowReadme,
    Pause,
    Interact,
    Restart,
    ExitToStartScreen,
    Exit,
}
public enum KeyPressMode {
    KeyDown,
    KeyUP,
    Both,
}