using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public RoomType _roomType;
    public DoorChecker[] _doors;
    public GameObject _content;
    public bool _found;
    public PolygonCollider2D _cameraBoundaries;
    public Action OnPlayerEnter;

    void Awake() {
        // foreach (var c in _doors) {
        // RaycastHit2D[] ray = new RaycastHit2D[10];
        // Physics2D.OverlapBox(c._checker.position, new(2, 2), 0);
        // }
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
