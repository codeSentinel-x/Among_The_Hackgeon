using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public WeaponHolder _weaponHolder;
    public float _rotSpeed;
    public Transform _spriteRenderer;
    // public Transform _firePoint;
    public Transform _bulletPref;


    public void Update() {
        RotateWeaponToMouse();
        HandleInput();
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weaponHolder.Shoot();
    }



    private void RotateWeaponToMouse() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        // direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < -90 || angle > 90) ChangeLocalScale(-1);
        else ChangeLocalScale(1);
        angle -= 90;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        _weaponHolder.transform.rotation = Quaternion.Lerp(_weaponHolder.transform.rotation, rot, _rotSpeed * Time.deltaTime);
    }
    void ChangeLocalScale(int x) {
        _weaponHolder.transform.GetChild(0).localScale = new(x, 1, 1);
        _spriteRenderer.localScale = new(Mathf.Abs(_spriteRenderer.localScale.x) * x, _spriteRenderer.localScale.y, 1);
    }
}
