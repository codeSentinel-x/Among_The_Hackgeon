using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils.Interfaces;
using MyUtils.ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable {
    public WeaponHolder _weaponHolder;
    public float _rotSpeed;
    public Transform _spriteRenderer;
    // public Transform _firePoint;
    public Transform _bulletPref;
    private float _maxHealth;
    private float _damageIgnore;
    private float _damageReduction;
    private float _currentHealth;
    private int _bullets;
    private bool _isReloading;
    private int _maxBullet;
    private float _reloadSpeedMult;
    private float _bulletSpeedMult;
    private float _shootDelayMult;
    void Awake() {
        var d = GetComponent<PlayerController>()._data;
        d._maxHealth._OnStatValueChanged += (x) => _maxHealth = x;
        d._damageIgnore._OnStatValueChanged += (x) => _damageIgnore = x;
        d._damageReduction._OnStatValueChanged += (x) => _damageReduction = x;
        d._reloadSpeedMult._OnStatValueChanged += (x) => _reloadSpeedMult = x;
        d._bulletSpeedMult._OnStatValueChanged += (x) => _bulletSpeedMult = x;
        d._shootDelayMult._OnStatValueChanged += (x) => _shootDelayMult = x;
    }
    void Start() {
        _currentHealth = _maxHealth;
    }

    public void Update() {
        RotateWeaponToMouse();
        HandleInput();
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weaponHolder.Shoot();
    }
    private IEnumerator Reload() {
        if (_isReloading) yield return null;
        yield return new WaitForSeconds(_reloadSpeedMult);
        Debug.Log("Reloaded");
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

    public void Damage(float v) {
        var v1 = v - _damageIgnore;
        var v2 = v1 - v1 * _damageReduction;
        _currentHealth -= v2;
        if (_currentHealth <= 0) Die();
        Debug.Log($"Base damage: {v}, After ignore: {v1}, After reduction {v2}");
    }

    private void Die() {
        Debug.Log("Player died"); ;
    }
}
