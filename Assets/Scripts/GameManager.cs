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
    public void ChangeMusicVolume(float value) => _gSettings._musicVolume = value;
    public void ChangeSoundVolume(float value) => _gSettings._soundsVolume = value;
    public void ChangeEnemySpeed(float value) => _gSettings._enemySpeedMultiplier = value;
    public void ChangePlayerSpeed(float value) => _gSettings._playerSpeedMultiplier = value;
    public void ChangeEnemyMaxHealth(float value) => _gSettings._enemyMaxHealthMultiplier = value;
    public void ChangePlayerMaxHealth(float value) => _gSettings._playerMaxHealthMultiplier = value;
    public void ChangeSpecialItemSpawnChange(float value) => _gSettings._specialItemSpawnChange = value;
    public void ChangeEnemyDamage(float value) => _gSettings._enemyDamageMultiplier = value;
    public void ChangePlayerDamage(float value) => _gSettings._playerDamageMultiplier = value;
    public void ChangeEnemyReduction(float value) => _gSettings._enemyDamageReductionMultiplier = value;
    public void ChangePlayerReduction(float value) => _gSettings._playerDamageReductionMultiplier = value;
    public void ChangeTimeMultiplier(float value) => _gSettings._timeMultiplier = value;
}
