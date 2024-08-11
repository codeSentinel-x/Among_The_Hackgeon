using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySO : MonoBehaviour {
    public EnemyAIType _aiType;
    public float _maxHealth;
    public float _baseDamage;
    public float _speed;
}
public enum EnemyAIType {
    Stupid,
    Normal,
    Advanced,
}
