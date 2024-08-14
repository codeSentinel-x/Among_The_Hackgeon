using System;
using System.Collections.Generic;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public RoomType _roomType;
    public DoorChecker[] _doors;
    public GameObject _content;
    public bool _found;
    public Transform[] _spawnPoints;
    public LayerMask _roomLayer;
    public PolygonCollider2D _cameraBoundaries;
    public bool _wasInvoked;
    public Action _onPlayerEnter;
    public static Action _onRoomClear;
    public List<Enemy> _enemies;
    void Start() {
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
        _content.SetActive(true);
    }
}
[Serializable]
public struct DoorChecker {
    public Rigidbody2D _checker;
    public DoorController _door;
}
