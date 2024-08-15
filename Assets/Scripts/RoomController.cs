using System;
using System.Collections.Generic;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public RoomType _roomType;
    public Transform _checkerTransform;
    public Transform _doorsTransform;
    private List<DoorChecker> _doors = new();
    public GameObject _roomMask;
    public bool _found;
    public Transform[] _spawnPoints;
    public LayerMask _roomLayer;
    public PolygonCollider2D _cameraBoundaries;
    public bool _wasInvoked;
    public Action _onPlayerEnter;
    public static Action _onRoomClear;
    public List<Enemy> _enemies;
    void Start() {
        if (_roomType == RoomType.Tunnel) return;
        if (_doorsTransform.Find("U") != null) _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("U").GetComponent<Rigidbody2D>(), _door = _doorsTransform.Find("U").GetComponent<DoorController>() });
        if (_doorsTransform.Find("R") != null) _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("R").GetComponent<Rigidbody2D>(), _door = _doorsTransform.Find("R").GetComponent<DoorController>() });
        if (_doorsTransform.Find("D") != null) _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("D").GetComponent<Rigidbody2D>(), _door = _doorsTransform.Find("D").GetComponent<DoorController>() });
        if (_doorsTransform.Find("L") != null) _doors.Add(new DoorChecker() { _checker = _checkerTransform.Find("L").GetComponent<Rigidbody2D>(), _door = _doorsTransform.Find("L").GetComponent<DoorController>() });
        
        foreach (var c in _doors) {
            var col = Physics2D.OverlapCircle(c._checker.position, 1, _roomLayer);
            if (col != null) {
                Debug.Log(col.gameObject.name);
                c._door._roomToShow1 = col.gameObject.GetComponent<RoomController>();
            }
        }
    }
    void Update() {
        if (_wasInvoked && _enemies.Count == 0) {
            _onRoomClear?.Invoke();
            _enemies.Add(new());
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (_wasInvoked) return;
            SetupRoom(other.gameObject.GetComponent<PlayerController>());
            _onPlayerEnter?.Invoke();
        }
    }

    private void SetupRoom(PlayerController contr) {
        if (contr._currentRoom == this) return;
        contr._confirmed.m_BoundingShape2D = _cameraBoundaries;
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
        foreach (var d in _doors) {
            // d._door.
        }
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
