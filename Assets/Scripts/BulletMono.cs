using MyUtils.Interfaces;
using MyUtils.Structs;
using UnityEngine;

public class BulletMono : MonoBehaviour {
    public int _bulletDamage = 10;
    public float _speed = 4;
    private Vector3 _startPos;
    public float _maxDist = 5;
    public LayerMask _layerToIgnore;
    public string _tagToIgnore;
    private Collider2D col;
    void Awake() {
        _startPos = transform.position;
        col = GetComponent<Collider2D>();
    }

    void Update() {
        float dist = Vector3.Distance(_startPos, transform.position);
        if (dist < _maxDist) {
            transform.localPosition += _speed * Time.deltaTime * Vector3.up;
        }
        else Destroy(transform.parent.gameObject);

    }
    public void Setup(BulletSetting s, float bSMult, LayerMask layerToIgnore, string tag) {
        _bulletDamage = s._damage;
        _speed = s._speed * bSMult;
        _maxDist = s._maxDist;
        _tagToIgnore = tag;
        _layerToIgnore = layerToIgnore;

    }
    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log($"Collision with{col.gameObject.name}");
        if (col.collider.isTrigger) return;
        if (col.gameObject.layer == this.gameObject.layer) return;
        if (col.gameObject.CompareTag(_tagToIgnore)) { Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), this.col); return; }

        IDamageable unit = col.gameObject.GetComponent<IDamageable>();
        unit?.Damage(_bulletDamage);
        Destroy(transform.parent.gameObject);


    }
}
