using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
    public string _sceneName;
    public TextMeshProUGUI _display;
    public float _time;
    public static Timer _I;
    private bool _spawned;
    public static Action _reload;
    public static List<GameObject> _objectToDestroy;
    public float _resetTime;
    void Start() {
        _objectToDestroy = new();
        _I = this;
        _time = _resetTime;
    }
    void Update() {
        _time -= Time.deltaTime;
        _display.text = _time.ToString("f0");
        if (_time < 9 && !_spawned) { Soundtrack._I.PlayLoopResetApproach(); Instantiate(GameDataManager._I._loopResetParticle, PlayerController._I.transform); _spawned = true; }
        if (_time <= 0) LoadScene();
    }
    public void LoadScene() {
        Soundtrack._I.PlayLoopReset();
        _time = _resetTime;
        _spawned = false;
        if (BossUI._I != null ? BossUI._I.gameObject : null != null) Destroy(BossUI._I.transform.parent.gameObject);
        foreach (var v in _objectToDestroy) if (v != null) Destroy(v);
        _objectToDestroy = new();
        _reload?.Invoke();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name /*== _sceneName ? _sceneName + 1 : _sceneName*/, LoadSceneMode.Single);
    }

}
