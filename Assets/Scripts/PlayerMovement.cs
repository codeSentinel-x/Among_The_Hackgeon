using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMovement : MonoBehaviour {
    public float _speed;
    private Vector2 _dir;
    private Rigidbody2D _rgb;

    void Awake() {
        _rgb = GetComponent<Rigidbody2D>();
    }

    public void Update() {
        HandleInput();
    }

    private void HandleInput() {
        _dir = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetButtonDown("Jump")) Dash();
    }

    private void Dash() {
        if(_dir != Vector3.zero )
    }

    void FixedUpdate() {
        _rgb.velocity = _dir.normalized * _speed;
    }
}
