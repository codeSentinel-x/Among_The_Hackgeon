using System;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public Transform _lightsHolder;
    public RoomType _roomType;
    public Transform _checkerTransform;
    public Transform _maskTransform;
    public Transform _doorsTransform;
    private List<DoorChecker> _doors = new();
    public GameObject _roomMask;
    public bool _found;
    public Transform[] _spawnPoints;
    public LayerMask _roomLayer;
    // public PolygonCollider2D _cameraBoundaries;
    public bool _wasInvoked;
    public Action _onPlayerEnter;
    public static Action<RoomController> _onRoomClear;
    public List<Enemy> _enemies;
    private int _enemyCount;
    public Transform[] _doorPrefab;
    public int _roomDifficulty;
    public int _wave;
    void Awake() {
        wasInvokedOnClear = true;
        _lightsHolder.gameObject.SetActive(false);
        try { _maskTransform = transform.Find("Mask"); } catch (SystemException e) { Debug.Log(e); }
        _doors = new();
        if (_roomType == RoomType.Tunnel) return;
        if (_checkerTransform.Find("R") != null) { var c = Instantiate(_doorPrefab[1], _doorsTransform).GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("R").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow = this; c._openedByDefault = true; c._doorType = DoorOpenType.AlwaysOpen; }
        if (_checkerTransform.Find("D") != null) { var c = Instantiate(_doorPrefab[2], _doorsTransform).GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("D").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow = this; c._openedByDefault = true; c._doorType = DoorOpenType.AlwaysOpen; }
        if (_checkerTransform.Find("L") != null) { var c = Instantiate(_doorPrefab[3], _doorsTransform).GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("L").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow = this; c._openedByDefault = true; c._doorType = DoorOpenType.AlwaysOpen; }
        if (_checkerTransform.Find("U") != null) { var c = Instantiate(_doorPrefab[0], _doorsTransform).GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("U").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow = this; c._openedByDefault = true; c._doorType = DoorOpenType.AlwaysOpen; }

        foreach (var c in _doors) {
            var col = Physics2D.OverlapCircle(c._checker.position, 1, _roomLayer);
            if (col != null) {
                Debug.Log(col.gameObject.name);
                c._door._tunnelToShow = col.gameObject.GetComponent<RoomController>();
                c._door.Initialize();
            }

        }
        if (_roomType == RoomType.EnemyRoom) {
            int count = Mathf.Clamp(UnityEngine.Random.Range(1, 1 + UnityEngine.Random.Range(_roomDifficulty, _roomDifficulty + 3)), _roomDifficulty, int.MaxValue);
            _spawnPoints = new Transform[count];
            for (int i = 0; i < count; i++) {
                var g = new GameObject("spawnPoint");
                g.transform.position = transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-6f, 6f));
                g.transform.SetParent(transform, true);
                _spawnPoints[i] = g.transform;

            }
        }
    }
    bool wasInvokedOnClear;
    void Update() {

    }
    public void EnemiesDie() {
        if (wasInvokedOnClear) return;
        _enemyCount -= 1;
        if (_enemyCount <= 0) {
            _wave -= 1;
            if (_wave == 0) {
                _onRoomClear?.Invoke(this);
                _enemies = new();
                wasInvokedOnClear = true;
            }
            else {
                SpawnEnemy();
            }
        }
    }
    public bool IsEmpty() {
        bool isEmpty = false;
        _enemies.ForEach(x => {
            if (x.gameObject.name == "Enemy") { isEmpty = true; };
        });
        return isEmpty;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _onPlayerEnter?.Invoke();
            _lightsHolder.gameObject.SetActive(true);
            // if (_wasInvoked) return;
            var p = other.gameObject.GetComponent<PlayerController>();
            p._currentRoom?.OnPlayerExit();
            SetupRoom(p);
        }
    }
    public void OnPlayerExit() {
        _lightsHolder.gameObject.SetActive(false);
    }

    private void SetupRoom(PlayerController contr) {
        if (contr._currentRoom == this) return;
        // contr._confirmed.m_BoundingShape2D = _cameraBoundaries;
        contr._currentRoom = this;
        Debug.Log($"Player entered {gameObject.name}");
        if (_roomType == RoomType.EnemyRoom && !_wasInvoked) SpawnEnemy();
    }
    private void SpawnEnemy() {
        if (_wave == 0) _wave = Mathf.Clamp(UnityEngine.Random.Range(1, 1 + UnityEngine.Random.Range(_roomDifficulty, _roomDifficulty + 2)), 1, 4);
        _wasInvoked = true;
        wasInvokedOnClear = false;
        foreach (var c in _spawnPoints) {
            var g = Instantiate(GameDataManager._I._enemyPref.GetEnemy(1), c.position, Quaternion.identity);
            var e = g.GetComponentInChildren<Enemy>();
            e._currentRoom = this;
            _enemies.Add(e);
        }
        _enemyCount = _enemies.Count;

        foreach (var d in _doors) {
            // d._door.
        }
    }
    public void ShowRoom() {
        if (_found) return;
        _found = true;
        if (_roomMask != null) _roomMask.SetActive(false);
    }
}
[Serializable]
public struct DoorChecker {
    public Rigidbody2D _checker;
    public DoorController _door;

}
