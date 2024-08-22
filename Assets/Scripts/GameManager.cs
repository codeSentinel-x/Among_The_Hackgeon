using System.Collections;
using System.Collections.Generic;
using MyUtils.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameSettings _gSettings;
    public GameObject _settings;
    void Awake() {
        try { _gSettings = SaveSystem.Load<GameSettings>(SaveSystem.SETTINGS_DEFAULT_SAVE_PATH, "defaultSettings"); } catch (System.Exception) { }
        DontDestroyOnLoad(this.gameObject);

    }
    public void LoadGame() {
        SceneManager.LoadScene(1);
    }
    public void OpenSetting() {
        _settings.SetActive(true);
        try { _gSettings = SaveSystem.Load<GameSettings>(SaveSystem.SETTINGS_DEFAULT_SAVE_PATH, "defaultSettings"); } catch (System.Exception) { }

    }
    public void CloseSetting() {
        _settings.SetActive(false);
        SaveSettings();
    }
    public void SaveSettings() {
        SaveSystem.Save<GameSettings>(SaveSystem.SETTINGS_DEFAULT_SAVE_PATH, "defaultSettings", _gSettings);
    }
}
