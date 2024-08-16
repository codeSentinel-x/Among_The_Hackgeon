using System;
using System.Collections.Generic;
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
    public static Action _onRoomClear;
    public List<Enemy> _enemies;
    private int _enemyCount;
    void Awake() {

        if (_roomType == RoomType.Tunnel) return;
        //TODO fix this and make this more readable
        if (_doorsTransform.Find("U") != null) { var c = _doorsTransform.Find("U").GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("U").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow2 = this; c.Initialize(); }
        if (_doorsTransform.Find("R") != null) { var c = _doorsTransform.Find("R").GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("R").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow2 = this; c.Initialize(); }
        if (_doorsTransform.Find("D") != null) { var c = _doorsTransform.Find("D").GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("D").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow2 = this; c.Initialize(); }
        if (_doorsTransform.Find("L") != null) { var c = _doorsTransform.Find("L").GetComponent<DoorController>(); _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("L").GetComponent<Rigidbody2D>(), _door = c }); c._roomToShow2 = this; c.Initialize(); }

        foreach (var c in _doors) {
            var col = Physics2D.OverlapCircle(c._checker.position, 1, _roomLayer);
            if (col != null) {
                Debug.Log(col.gameObject.name);
                c._door._roomToShow1 = col.gameObject.GetComponent<RoomController>();
            }
        }
    }
    bool wasInvokedOnClear;
    void Update() {
        if (wasInvokedOnClear) return;
        if (_wasInvoked && _enemyCount <= 0) {
            _onRoomClear?.Invoke();
            _enemies = new();
            wasInvokedOnClear = true;
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
        _wasInvoked = true;
        foreach (var c in _spawnPoints) {
            var g = Instantiate(GameDataManager._I._enemyPref, c.position, Quaternion.identity);
            var e = g.GetComponentInChildren<Enemy>();
            e._currentRoom = this;
            _enemies.Add(e);
        }
        _enemyCount = _enemies.Count;
        foreach (var d in _doors) {
            // d._door.
        }
    }
    public void OnEnemyKill() {
        _enemyCount -= 1;
    }
    public void ShowRoom() {
        if (_found) return;
        _found = true;
        _roomMask?.SetActive(false);
    }
}
[Serializable]
public struct DoorChecker {
    public Rigidbody2D _checker;
    public DoorController _door;

}
