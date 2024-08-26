using MyUtils.Enums;
using MyUtils.Interfaces;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Obstacles : MonoBehaviour, IDamageable, IInteractable {
    public ObstacleType _type;
    public int _durability;
    public int _pushPower;
    private BoxCollider2D _collider;
    private Rigidbody2D _rgb;
    private SpriteRenderer _renderer;
    public ParticleSystem _explosionParticle;
    public ParticleSystem _damageParticle;

    void Awake() {
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _rgb = GetComponent<Rigidbody2D>();
    }
    public void Damage(float v) {
        Instantiate(_damageParticle, transform.position, Quaternion.identity);
        switch (_type) {
            case ObstacleType.PushableExploding: {
                    _durability -= 1;
                    if (_durability <= 0) Explode();
                    break;
                }
            case ObstacleType.PushableDestroyable: {
                    _durability -= 1;
                    if (_durability <= 0) Destroy();
                    break;
                }
            case ObstacleType.StaticDestroyable: {
                    _durability -= 1;
                    if (_durability <= 0) Destroy();
                    break;
                }
        }
    }

    private void Explode() {
        Debug.Log($"{name} exploded");
        Instantiate(_explosionParticle, transform.position, Quaternion.identity);
    }

    public void Interact() {
        Debug.Log($"{name} interacted with player");
        switch (_type) {
            case ObstacleType.Pushable: {
                    _rgb.AddForce((transform.position - PlayerController._I.transform.position).normalized * _pushPower, ForceMode2D.Impulse);
                    break;
                }
            case ObstacleType.PushableExploding: {
                    _rgb.AddForce((transform.position - PlayerController._I.transform.position).normalized * _pushPower, ForceMode2D.Impulse);
                    break;

                }
            case ObstacleType.PushableDestroyable: {
                    _rgb.AddForce((transform.position - PlayerController._I.transform.position).normalized * _pushPower, ForceMode2D.Impulse);
                    break;
                }
        }
    }
    public float _force;

    public void Destroy() {
        Debug.Log($"{name} was destroyed");
        Destroy(gameObject);
    }
}
