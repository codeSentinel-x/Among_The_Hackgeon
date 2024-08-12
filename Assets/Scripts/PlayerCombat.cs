using System.Collections;
using System.Collections.Generic;
using MyUtils.Classes;
using MyUtils.Interfaces;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable {

    [Header("Setup")]
    public Transform _weaponHolder;
    public Transform _firePoint;
    public Transform _spriteRenderer;
    public SpriteRenderer _weaponSpriteR;
    public string _defaultWeaponName;
    public float _rotSpeed;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private List<Weapon> _weapons = new();
    private float _maxHealth;
    private float _damageIgnore;
    private float _damageReduction;
    private float _currentHealth;
    private bool _isReloading;
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
        _currentWeapon = new(GameDataManager.LoadByName(_defaultWeaponName));
        _currentWeapon.Setup(_firePoint, _weaponSpriteR);
        _weapons.Add(_currentWeapon);
        _weapons.Add(new(GameDataManager.LoadByName("Eagle")));

        _currentWeaponIndex = _weapons.Count - 1;
        _currentHealth = _maxHealth;
        ReseTBulletDisplay();
        PlayerUI._I.ChangeWeapon(_currentWeapon._defaultSettings._sprite);
    }

    public void Update() {
        HandleInput();
    }
    void LateUpdate() {
        RotateWeaponToMouse();
    }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();
        if (Input.GetKeyDown(KeyCode.R) && !_isReloading) StartCoroutine(Reload());
        if (Input.GetKeyDown(KeyCode.LeftShift)) NextWeapon();
        if (Input.GetKeyDown(KeyCode.LeftControl)) PreviousWeapon();
    }
    public void Shoot() {
        if (_isReloading) return;
        if (_currentWeapon._nextShoot > Time.time) return;
        if (_currentWeapon._bulletsInMagazine <= 0) { StartCoroutine(Reload()); Debug.Log("No bullets"); return; }
        Debug.Log("Piu");
        var b = Instantiate(_currentWeapon._defaultSettings._bulletPref, _firePoint.position, _weaponHolder.rotation).GetComponentInChildren<BulletMono>();
        b.Setup(_currentWeapon._defaultSettings._bulletSetting, _bulletSpeedMult, gameObject.layer, "Player");
        Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        _currentWeapon.Shoot(_shootDelayMult);
        PlayerUI._I.DecaresBullet();

    }
    public void NextWeapon() {
        if (_isReloading) return;
        _currentWeaponIndex += 1;
        if (_currentWeaponIndex >= _weapons.Count) _currentWeaponIndex = 0;
        _currentWeapon = _weapons[_currentWeaponIndex];
        _currentWeapon.Setup(_firePoint, _weaponSpriteR);
        ReseTBulletDisplay();
        PlayerUI._I.ChangeWeapon(_currentWeapon._defaultSettings._sprite);


    }
    public void PreviousWeapon() {
        if (_isReloading) return;
        _currentWeaponIndex -= 1;
        if (_currentWeaponIndex < 0) _currentWeaponIndex = _weapons.Count - 1;
        _currentWeapon = _weapons[_currentWeaponIndex];
        _currentWeapon.Setup(_firePoint, _weaponSpriteR);
        ReseTBulletDisplay();
        PlayerUI._I.ChangeWeapon(_currentWeapon._defaultSettings._sprite);

    }


    private IEnumerator Reload() {
        if (_isReloading) yield return null;
        _isReloading = true;
        StartCoroutine(PlayerUI._I.DisplayReload(_currentWeapon._reloadTime * _reloadSpeedMult));
        yield return new WaitForSeconds(_currentWeapon._reloadTime * _reloadSpeedMult);
        _currentWeapon.Reload();
        Debug.Log("Reloaded");
        _isReloading = false;
        ReseTBulletDisplay();
    }

    void ReseTBulletDisplay() => PlayerUI._I.ResetBullets(_currentWeapon._bulletsInMagazine);

    private void RotateWeaponToMouse() {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, -10));
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
