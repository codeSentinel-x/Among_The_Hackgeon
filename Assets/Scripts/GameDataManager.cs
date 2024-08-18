using System;
using NUnit.Framework;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _I;
    public Sprite[] _destroyableDoorSpritesHorizontal;
    public Sprite[] _destroyableDoorSpritesVerticalLeft;
    public Sprite[] _destroyableDoorSpritesVerticalRight;
    public Sprite[] _closedDoorSprite;
    public Sprite[] _openedDoorSprite; //0 - up; 1- right, 2 - down; 3 - left
    public EnemyPrefabGetter _enemyPref;
    public Sprite _bulletSprite;
    public Sprite _blankSprite;
    public Transform _damageParticle;
    public Canvas 
    void Awake() {
        _I = this;
    }
    public static WeaponSO LoadWeaponByName(string name) => Resources.Load<WeaponSO>($"Weapons/{name}");
    public static SpecialItemSO LoadItemByName(string name) => Resources.Load<SpecialItemSO>($"Items/{name}");

}
[Serializable]
public struct EnemyPrefabGetter {
    public EnemySpawnChange[] _enemies;

    public Transform GetEnemy(int mult) {
        var r = UnityEngine.Random.Range(0, 100) * mult;
        Debug.Log(r);
        foreach (var e in _enemies) {
            if (e._spawnChange <= r) return e._enemyPref;
        }
        return _enemies[0]._enemyPref;
    }
}
[Serializable]
public struct EnemySpawnChange {
    public float _spawnChange;
    public Transform _enemyPref;
}
