using System;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public RoomType _roomType;
    public DoorChecker[] _doors;
    public GameObject _content;
    public bool _found;
    public LayerMask _roomLayer;
    public PolygonCollider2D _cameraBoundaries;
    public Action OnPlayerEnter;

    void Start() {
        foreach (var c in _doors) {
            var col = Physics2D.OverlapCircle(c._checker.position, 1, _roomLayer);
            if (col != null) {
                Debug.Log(col.gameObject.name);
                c._door._roomToShow1 = col.gameObject.GetComponent<RoomController>();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SetupRoom(other.gameObject.GetComponent<PlayerController>());
            OnPlayerEnter?.Invoke();
        }
    }

    private void SetupRoom(PlayerController contr) {
        if (contr._currentRoom == this) return;
        contr._confirmed.m_BoundingShape2D = _cameraBoundaries;
        contr._currentRoom = this;
        Debug.Log($"Player entered {gameObject.name}");
        SpawnEnemy();
    }
    private void SpawnEnemy() {
        var g = Instantiate(GameDataManager._I._enemyPref, transform.position, Quaternion.identity);
        g.GetComponentInChildren<Enemy>()._currentRoom = this;
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
