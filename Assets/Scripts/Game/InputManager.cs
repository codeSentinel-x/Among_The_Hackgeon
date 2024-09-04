using System.Runtime.Serialization.Formatters;
using MyUtils.Classes;
using UnityEngine;
public class InputManager : MonoBehaviour {
    public const string KEY_BIND_NAME = "KeyBinds";
    public static InputManager _I;
    public static KeyBindData _keyBindData;
    public KeyBindArrayElement[] _keyBinds;
    void Awake() {
        _keyBindData = SaveSystem.Load<KeyBindData>(SaveSystem.KEY_BIND_DATA_SAVE_PATH, KEY_BIND_NAME);
        if (_keyBindData == null) {
            foreach (var k in _keyBinds) {
                _keyBindData._keyBinds.Add(k._type, k._keyBind);
            }
            SaveKeyBinds();
            _keyBindData = SaveSystem.Load<KeyBindData>(SaveSystem.KEY_BIND_DATA_SAVE_PATH, KEY_BIND_NAME);

        }
    }
    void OnApplicationQuit() {
        SaveKeyBinds();
    }
    public static void SaveKeyBinds() {
        SaveSystem.Save(SaveSystem.KEY_BIND_DATA_SAVE_PATH, KEY_BIND_NAME, _keyBindData);
    }
}
