using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/Enemy")]
public class EnemySO : ScriptableObject {
    public EnemyAIType _aiType;
    public float _maxHealth;
    public float _baseDamage;
    public float _speed;
    public WeaponSO _defaultWeapon;
}
public enum EnemyAIType {
    Stupid,
    Normal,
    Advanced,
}
