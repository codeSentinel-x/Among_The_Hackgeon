using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    public Transform _weaponHolder;
    public 
    public Transform _firePoint;
    public Transform _bulletPref;

    public void Update() {
        RotateWeaponToMouse();
    }

    private void RotateWeaponToMouse() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - _firePoint.position;
        //direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        _weaponHolder.rotation = Quaternion.Slerp(_firePoint.rotation, rot, _rotSpeed.GetValue() * Time.deltaTime);
    }
}
