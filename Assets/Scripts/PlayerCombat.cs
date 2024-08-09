using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.ScriptableObjects;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public WeaponHolder _weaponHolder;
    public float _rotSpeed;
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
        Vector2 direction = mousePos - _weaponHolder.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        if (angle < -90 || angle > 90) _weaponHolder.transform.GetChild(0).localScale = new(-1, 1, 1);
        else _weaponHolder.transform.GetChild(0).localScale = new(1, 1, 1);
        angle -= 90;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        _weaponHolder.transform.rotation = Quaternion.Slerp(_weaponHolder.transform.rotation, rot, _rotSpeed * Time.deltaTime);
    }
}
