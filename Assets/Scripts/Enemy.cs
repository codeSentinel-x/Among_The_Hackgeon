using System.Collections;
using MyUtils.Classes;
using MyUtils.Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    public RoomController _currentRoom;
    public EnemySO _defaultSetting;
    public Weapon _weapon;
    public Transform _target;
    public Transform _firePoint;
    public Transform _weaponHolder;
    public SpriteRenderer _weaponSR;
    public Vector2 _moveDirection;
    public float _nextMoveDirectionChange;
    public float _minPlayerDist;
    private Rigidbody2D _rgb;
    private bool _isReloading;
    public Transform _spriteRenderer;
    private int _delayIndex;
    private float _nextShootTime;
    private float _currentHealth;

    public void Awake() {
        _weapon = new(_defaultSetting._defaultWeapon);
        _weapon.Setup(_firePoint, _weaponSR);
        _target = PlayerController._I.transform;
        _currentHealth = _defaultSetting._maxHealth;
        _rgb = GetComponent<Rigidbody2D>();
        _weapon._bulletsInMagazine = Random.Range(0, _weapon._defaultSettings._maxBullet + 1);
        _nextShootTime = Time.time + Random.Range(0.2f, 0.8f);
    }
    void Update() {
        RotateWeaponToPlayer();
        if (_nextShootTime < Time.time) Shoot();
        if (_nextMoveDirectionChange > Time.time) return;
        Debug.Log($"Move direction: {Vector2.Distance(_target.position, transform.position)}");
        if (Vector2.Distance(_target.position, transform.position) > _minPlayerDist) {
            _moveDirection = _target.position - transform.position;
            _nextMoveDirectionChange = Time.time + Random.Range(2f, 5f);
        }
        else {
            Vector2 newVec = new(Mathf.Clamp(transform.position.x - Random.Range(-6f, 6f), _currentRoom.transform.position.x - 11, _currentRoom.transform.position.x + 11), transform.position.y - Mathf.Clamp(Random.Range(-6f, 6f), _currentRoom.transform.position.y - 8, _currentRoom.transform.position.y + 8));
            _moveDirection = newVec - (Vector2)transform.position;
            _nextMoveDirectionChange = Time.time + Random.Range(2f, 5f);
        }
    }
    void FixedUpdate() {
        _rgb.velocity = _moveDirection.normalized * _defaultSetting._speed;
    }
    public void Shoot() {
        if (_isReloading) return;
        if (_weapon._nextShoot > Time.time) return;
        if (_weapon._bulletsInMagazine <= 0) { StartCoroutine(Reload()); Debug.Log("No bullets"); return; }
        Debug.Log("Piu");
        var b = Instantiate(_weapon._defaultSettings._bulletPref, _firePoint.position, _weaponHolder.rotation).GetComponentInChildren<BulletMono>();
        b.Setup(_weapon._defaultSettings._bulletSetting, 1, gameObject.layer, "Enemy");
        Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        _weapon.Shoot(3);
        _nextShootTime = Time.time + _defaultSetting._shootDelays[_delayIndex];
        _delayIndex++;
        if (_delayIndex >= _defaultSetting._shootDelays.Count) _delayIndex = 0;

    }
    private IEnumerator Reload() {
        if (_isReloading) yield return null;
        _isReloading = true;
        yield return new WaitForSeconds(_weapon._reloadTime * 3);
        _weapon.Reload();
        Debug.Log("Reloaded");
        _isReloading = false;
    }
    private void RotateWeaponToPlayer() {

        Vector2 direction = _target.transform.position - transform.position;
        // direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < -90 || angle > 90) ChangeLocalScale(-1);
        else ChangeLocalScale(1);
        angle -= 90;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        _weaponHolder.transform.rotation = Quaternion.Lerp(_weaponHolder.transform.rotation, rot, 100 * Time.deltaTime);
    }
    void ChangeLocalScale(int x) {
        _weaponHolder.transform.GetChild(0).localScale = new(x, 1, 1);
        _spriteRenderer.localScale = new(Mathf.Abs(_spriteRenderer.localScale.x) * x, _spriteRenderer.localScale.y, 1);
    }

    public void Damage(float v) {
        _currentHealth -= v;
        if (_currentHealth <= 0) Die();
    }
    public void Die() {
        _currentRoom._enemies.Remove(this);
        _currentRoom.OnEnemyKill();
        Destroy(transform.parent.gameObject);
    }
}
