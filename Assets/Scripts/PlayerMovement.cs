using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour {
    public float _speed;
    public float _dashDuration;
    public float _dashPower;
    public float _maxStamina;
    public float _stamRegPerSec;
    private bool _canMove = true;
    private Vector2 _dir;
    private Rigidbody2D _rgb;
    public bool _dashToMouse;
    private float _currentStamina;
    void Awake() {
        _rgb = GetComponent<Rigidbody2D>();
        var p = GetComponent<PlayerController>();
        p._data._dashDuration._OnStatValueChanged += (x) => _dashDuration = x;
        p._data._movementSpeed._OnStatValueChanged += (x) => _speed = x;
        p._data._dashPower._OnStatValueChanged += (x) => _dashPower = x;
        p._data._stamRegPerSecMult._OnStatValueChanged += (x) => _stamRegPerSec = x;
        p._data._maxStamina._OnStatValueChanged += (x) => _maxStamina = x;

        //todo {
        p._data._maxStamina._OnStatValueChanged += (x) => _maxStamina = x;
        p._data._maxStamina._OnStatValueChanged += (x) => _maxStamina = x;
        p._data._maxStamina._OnStatValueChanged += (x) => _maxStamina = x;
        //todo }


    }

    public void Update() {
        HandleInput();
    }

    private void HandleInput() {
        _dir = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Mouse1)) if (_dashToMouse) DashToMouse(Camera.main.ScreenToWorldPoint(Input.mousePosition)); else DashForward();
    }
    private void DashForward() {
        _rgb.AddForce(_dir.normalized * _dashPower, ForceMode2D.Impulse);
        StartCoroutine(StopMoving(_dashDuration));
    }
    private void DashToMouse(Vector2 pos) {
        Vector2 dir = pos - new Vector2(transform.position.x, transform.position.y);
        if (dir == Vector2.zero || !_canMove) return;
        Debug.Log(dir.ToString());
        _rgb.AddForce(dir.normalized * _dashPower, ForceMode2D.Impulse);
        // Debug.Log("dash");
        StartCoroutine(StopMoving(_dashDuration));
    }

    void FixedUpdate() {
        if (!_canMove) return;
        _rgb.velocity = _dir.normalized * _speed;
    }
    IEnumerator StopMoving(float time) {
        _canMove = false;
        yield return new WaitForSeconds(time);
        _canMove = true;
    }
}
