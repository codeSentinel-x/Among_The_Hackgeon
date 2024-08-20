using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    public string _sceneName;
    public TextMeshProUGUI _display;
    public float _time;
    public static Timer _I;
    private bool _spawned;
    public static Action _reload;
    public static List<GameObject> _objectToDestroy;
    void Start() {
        _objectToDestroy = new();
        _I = this;
        _time = 333f;
    }
    void Update() {
        _time -= Time.deltaTime;
        _display.text = _time.ToString("f0");
        if (_time < 9 && !_spawned) { Instantiate(GameDataManager._I._loopResetParticle, PlayerController._I.transform); _spawned = true; }
        if (_time <= 0) LoadScene();
    }
    public void LoadScene() {
        _time = 333f;
        _spawned = false;
        foreach (var v in _objectToDestroy) if (v != null) Destroy(v.transform.parent.gameObject);
        _objectToDestroy = new();
        _reload?.Invoke();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name /*== _sceneName ? _sceneName + 1 : _sceneName*/, LoadSceneMode.Single);
    }

}
