using System.Collections;
using System.Collections.Generic;
using MyUtils.Enums;
using MyUtils.Interfaces;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Obstacles : MonoBehaviour, IDamageable, IInteractable
{
    public ObstacleType _type;
    public int _durability;
    public int _pushPower;
    private BoxCollider2D _collider;
    private Rigidbody2D _rgb;
    private SpriteRenderer _renderer;
    void Awake(){
        _collider = GetComponent<BoxCollider2D>();        
        _renderer = GetComponent<SpriteRenderer>();
        _rgb = GetComponent<Rigidbody2D>();
    }
    public void Damage(float v) {
        _durability -= 1;
        if (_durability <= 0) Destroy();
    }
    public void Interact() {
        _rgb.AddForce((transform.position - PlayerController._I.transform.position).normalized * _pushPower, ForceMode2D.Impulse);
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}
