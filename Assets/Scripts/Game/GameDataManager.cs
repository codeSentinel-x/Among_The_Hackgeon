using System;
using UnityEngine;

public enum WeaponType {
    Single,
    Sniper,
    Auto,
    MachineGun,
}
public class GameDataManager : MonoBehaviour {

    [Header("Prefabs")]
    public static GameDataManager _I;
    public Transform _dungeonPrefab;
    public Transform _playerPrefab;
    public Transform _bossPrefab;
    public EnemyPrefabGetter _enemyPref;

    [Header("Door sprites")]
    public Sprite[] _destroyableDoorSpritesHorizontal;
    public Sprite[] _destroyableDoorSpritesVerticalLeft;
    public Sprite[] _destroyableDoorSpritesVerticalRight;
    public Sprite[] _closedDoorSprite;
    public Sprite[] _openedDoorSprite; //0 - up; 1- right, 2 - down; 3 - left

    [Header("Enemy particles")]
    public Transform _spawnParticle;
    public Transform _collisionParticle;
    public Transform _loopResetParticle;
    public Transform _damageParticle;
    [Header("Sprites")]
    public Sprite _bulletSprite;
    public Sprite _blankSprite;
    public Canvas _endCanvas;

    [Header("Items")]
    public Transform[] _weaponsPrefab;
    public Transform[] _specialItemPrefab;
    public Transform[] _UtilityItemPrefab;
    public Transform _chestPrefab;
    public Transform _bossKeyPrefab;

 
    GameObject _dungeon;
    GameObject _player;

    void Awake() {
        _dungeon = Instantiate(_dungeonPrefab, Vector3.zero, Quaternion.identity).gameObject;
        _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity).gameObject;
        _I = this;
        Timer._reload += () => { Destroy(_dungeon); Destroy(_player); _dungeon = Instantiate(_dungeonPrefab, Vector3.zero, Quaternion.identity).gameObject; _player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity).gameObject; };
    }
    public static WeaponSO LoadWeaponByName(string name) => Resources.Load<WeaponSO>($"Weapons/{name}");
    public static SpecialItemSO LoadItemByName(string name) => Resources.Load<SpecialItemSO>($"Items/{name}");
    public void InstantiateParticles(Vector3 pos, Transform pref) => Instantiate(pref, pos, Quaternion.identity);
    public void InstantiateParticles(Transform parent, Transform pref) => Instantiate(parent, pref);


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
