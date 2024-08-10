using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.Enums;
using UnityEngine;

public class RoomController : MonoBehaviour {
    public RoomType _roomType;
    public GameObject _content;
    public bool _found;
    public PolygonCollider2D _cameraBoundaries;
    public DoorController[] _doors;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SetupRoom(other.gameObject.GetComponent<PlayerController>());
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
