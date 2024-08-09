using System.Collections;
using System.Collections.Generic;
using MyUtils.ScriptableObjects;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

public class WeaponHolder : MonoBehaviour {
    public WeaponSO _defaultWeapon;
    public Transform _firePoint;
    void Awake() {
        InitializeWeapon();
    }
    public void InitializeWeapon() {
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = _defaultWeapon._sprite;
        _firePoint.localPosition = _defaultWeapon._firePointPos;
    }
    public void Shoot() {
        Debug.Log("Piu");
        Instantiate(_defaultWeapon._bulletPref, _firePoint.position, transform.rotation);
    }
}
