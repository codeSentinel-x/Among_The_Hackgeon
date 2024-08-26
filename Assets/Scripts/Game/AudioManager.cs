using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager _I { get; private set; }
    public Transform _soundPlayerPrefabs;
    #region Audio clips
    [Header("Player sounds")]
    public AudioClip _playerDamageSound;
    public AudioClip _weaponChangeSound;
    public AudioClip _dashSound;
    public AudioClip _blankSound;
    public AudioClip _playerHealSound;
    public AudioClip _pickupSound;
    public AudioClip _reloadStartSound;
    public AudioClip _reloadEndSound;
    public AudioClip[] _shootSounds;
    [Header("Enemy sounds")]
    public AudioClip _enemySpawnSound;
    public AudioClip _enemyDamageSound;
    public AudioClip _enemyDieSound;
    public AudioClip _bossDie;
    [Header("Dungeon sounds")]
    public AudioClip _doorOpenSound;
    public AudioClip _approachingLoopReset;
    public AudioClip _loopResetSound;
    public AudioClip _loopBreak;
    [Header("Soundtrack")]
    public AudioClip[] _startScreenSound;
    public AudioClip[] _defaultSound;
    public AudioClip[] _combatSound;
    public AudioClip GetWeaponSound(WeaponType type) {
        switch (type) {
            case WeaponType.Single: {
                    return _shootSounds[0];
                }
            case WeaponType.Sniper: {
                    return _shootSounds[1];
                }
            case WeaponType.Auto: {
                    return _shootSounds[2];
                }
            case WeaponType.MachineGun: {
                    return _shootSounds[3];
                }
            default: return null;
        }
    }
    #endregion
    void Awake() {
        _I = this;
        // DontDestroyOnLoad(gameObject);
    }
    public void PlaySoundEffect(Vector3 pos, AudioClip clip) {
        var s = Instantiate(_soundPlayerPrefabs, pos, Quaternion.identity).GetComponent<SoundPlayer>();
        s.Play(clip);

    }
    public AudioClip GetNormalByIndex(int i) => _defaultSound[i];

}
