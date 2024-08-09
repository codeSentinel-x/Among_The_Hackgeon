using System.Collections;
using System.Collections.Generic;
using MyUtils.Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int _bulletDamage = 10;
    public float _speed = 4;
    private Vector3 _startPos;
    public float _maxDist = 5;
    public LayerMask layerToIgnore;
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
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.layer == layerToIgnore) {
            Physics2D.IgnoreCollision(col.collider, this.col);
        }
        else {
            IDamageable unit = col.gameObject.GetComponent<IDamageable>();
            unit?.Damage(_bulletDamage);
            Destroy(transform.parent.gameObject);

        }

    }
}
