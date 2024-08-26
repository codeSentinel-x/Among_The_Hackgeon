using UnityEngine;
public class AudioManager : MonoBehaviour {
    public static AudioManager _I;
    public Transform _soundPlayerPrefabs;

    [Header("Player sounds")]
    public AudioClip _playerDamageSound;
    public AudioClip _playerWeaponChangeSound;
    public AudioClip _playerDashSound;
    public AudioClip _playerBlankSound;
    public AudioClip _playerHealSound;
    public AudioClip _playerPickupSound;
    public AudioClip _playerReloadStartSound;
    public AudioClip _playerReloadEndSound;
    public AudioClip[] _playerShootSounds;
    [Header("Enemy sounds")]
    public AudioClip _enemySpawnSound;
    public AudioClip _enemyDamageSound;
    public AudioClip _enemyDieSound;
    public AudioClip[] _enemyShootSounds;
    [Header("Boss sounds")]
    public AudioClip _bossSpawnSound;
    public AudioClip _bossDamageSound;
    public AudioClip _bossInvincibleStartSound;
    public AudioClip _bossInvincibleEndSound;
    public AudioClip _bossDieSound;
    public AudioClip[] _bossShootSounds;
    [Header("Dungeon sounds")]
    public AudioClip _doorOpenSound;
    public AudioClip _approachingLoopResetSound;
    public AudioClip _loopResetSound;
    public AudioClip _loopBreakSound;
    [Header("Soundtrack")]
    public AudioClip[] _startScreenMusic;
    public AudioClip[] _defaultMusic;
    public AudioClip[] _combatMusic;
    void Awake() {
        _I = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySoundEffect(AudioType type, Vector3 pos) {
        var s = Instantiate(_soundPlayerPrefabs, pos, Quaternion.identity).GetComponent<SoundPlayer>();
        s.Play(GetSound(type));

    }
    public void PlaySoundEffect(AudioType type, Transform parent) {
        var s = Instantiate(_soundPlayerPrefabs, parent).GetComponent<SoundPlayer>();
        s.Play(GetSound(type));

    }
    public void PlaySoundEffect(AudioType aType, WeaponType wType, Vector3 position) {
        var s = Instantiate(_soundPlayerPrefabs, position, Quaternion.identity).GetComponent<SoundPlayer>();
        s.Play(GetSound(aType, wType));

    }
    public AudioClip GetSound(AudioType type) {
        return type switch {
            AudioType.PlayerDamage => _playerDamageSound,
            AudioType.PlayerWeaponChange => _playerWeaponChangeSound,
            AudioType.PlayerBlank => _playerBlankSound,
            AudioType.PlayerHeal => _playerHealSound,
            AudioType.PlayerPickup => _playerPickupSound,
            AudioType.PlayerReloadStart => _playerReloadStartSound,
            AudioType.PlayerReloadEnd => _playerReloadEndSound,
            AudioType.EnemySpawn => _enemySpawnSound,
            AudioType.EnemyDamage => _playerDamageSound,
            AudioType.EnemyDie => _enemyDieSound,
            AudioType.BossSpawn => _bossSpawnSound,
            AudioType.BossDamage => _bossDamageSound,
            AudioType.BossInvincibleStart => _bossInvincibleStartSound,
            AudioType.BossInvincibleEnd => _bossInvincibleEndSound,
            AudioType.BossDie => _bossDieSound,
            AudioType.DoorOpen => _doorOpenSound,
            AudioType.ApproachingLoopReset => _approachingLoopResetSound,
            AudioType.LoopReset => _loopResetSound,
            AudioType.LoopBreak => _loopBreakSound,
            _ => null,
        };
    }
    public AudioClip GetSound(AudioType aType, WeaponType wType) {
        return aType switch {
            AudioType.PlayerShoot => GetWeaponSound(wType, "player"),
            AudioType.EnemyShoot => GetWeaponSound(wType, "enemy"),
            AudioType.BossShoot => GetWeaponSound(wType, "boss"),
            _ => null,
        };
    }
    public AudioClip GetWeaponSound(WeaponType type, string name) {
        AudioClip[] array = name switch {
            "player" => _playerShootSounds,
            "enemy" => _enemyShootSounds,
            "boss" => _bossShootSounds,
            _ => throw new System.ArgumentException($"No sounds for {name}"),
        };
        return type switch {
            WeaponType.Single => array.Length > 0 ? array[0] : null,
            WeaponType.Sniper => array.Length > 1 ? array[1] : null,
            WeaponType.Auto => array.Length > 2 ? array[2] : null,
            WeaponType.MachineGun => array.Length > 3 ? array[3] : null,
            _ => null,
        };

    }
    public AudioClip GetNormalByIndex(int i) => _defaultMusic[i];

}
public enum AudioType {
    PlayerDamage,
    PlayerWeaponChange,
    PlayerDash,
    PlayerBlank,
    PlayerHeal,
    PlayerPickup,
    PlayerReloadStart,
    PlayerReloadEnd,
    PlayerShoot,
    EnemySpawn,
    EnemyDamage,
    EnemyDie,
    EnemyShoot,
    BossSpawn,
    BossDamage,
    BossInvincibleStart,
    BossInvincibleEnd,
    BossDie,
    BossShoot,
    DoorOpen,
    ApproachingLoopReset,
    LoopReset,
    LoopBreak,
}
