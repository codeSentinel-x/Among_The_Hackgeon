using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType {
    ChestSpawn,
    ChestOpen,
    EnemySpawn,
    EnemyDamage,
    EnemyDie,
    BossSpawn,
    BossDamage,
    BossStageChange,
    BossInvincibleStop,
    BossDie,
    PlayerSpawn,
    PlayerDamage,
    PlayerBlank,
    CollisionParticle,
    LoopReset,
}
public class ParticleAssetManager : MonoBehaviour {
    public static ParticleAssetManager _I;
    [Header("Chest particles")]
    public Transform _chestSpawnParticle;
    public Transform _chestOpenParticle;

    [Header("Enemy particles")]
    public Transform _enemySpawnParticle;
    public Transform _enemyDamageParticle;
    public Transform _enemyDieParticle;
    [Header("Boss particles")]
    public Transform _bossSpawnParticle;
    public Transform _bossDamageParticle;
    public Transform _bossStageChangeParticle;
    public Transform _bossInvincibleStopParticle;
    public Transform _bossDieParticle;

    [Header("Player particles")]
    public Transform _playerSpawnParticle;
    public Transform _playerDamageParticle;
    public Transform _playerBlankParticle;

    [Header("Others")]
    public Transform _collisionParticle;
    public Transform _loopResetParticle;


    public Transform GetParticle(ParticleType type) {
        return type switch {
            ParticleType.ChestSpawn => _chestSpawnParticle,
            ParticleType.ChestOpen => _chestOpenParticle,
            ParticleType.EnemySpawn => _enemySpawnParticle,
            ParticleType.EnemyDamage => _enemyDamageParticle,
            ParticleType.EnemyDie => _enemyDieParticle,
            ParticleType.BossSpawn => _bossSpawnParticle,
            ParticleType.BossDamage => _bossDamageParticle,
            ParticleType.BossStageChange => _bossStageChangeParticle,
            ParticleType.BossDie => _bossDieParticle,
            ParticleType.BossInvincibleStop => _bossInvincibleStopParticle,
            ParticleType.PlayerSpawn => _playerSpawnParticle,
            ParticleType.PlayerDamage => _playerSpawnParticle,
            ParticleType.PlayerBlank => _playerBlankParticle,
            ParticleType.CollisionParticle => _collisionParticle,
            ParticleType.LoopReset => _loopResetParticle,
            _ => null,
        };
    }
    public void InstantiateParticles(ParticleType type, Vector3 pos) => Instantiate(GetParticle(type), pos, Quaternion.identity);
    public void InstantiateParticles(ParticleType type, Transform parent) => Instantiate(GetParticle(type), parent);

    void Awake() {
        _I = GetComponent<ParticleAssetManager>();
    }

}
