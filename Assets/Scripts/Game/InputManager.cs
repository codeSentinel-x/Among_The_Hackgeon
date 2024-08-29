using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityEvent OnKeyPressed(KeyBindType type) {
        _keyDownBinds.TryGetValue(type, out var key);
        return key._onKeyAction;
    }
    public void ChangeKeyBind(KeyCode newBind){
        
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