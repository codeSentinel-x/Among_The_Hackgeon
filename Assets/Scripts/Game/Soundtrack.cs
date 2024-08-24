using MyUtils.Functions;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
    public static Soundtrack _I;
    public int index;

    public AudioSource _startScreenSource;
    public AudioSource _normalSource;
    public AudioSource _loopSource;
    public AudioSource _combatSource;
    public bool _isCombat;
    private GameAudioManager _gAM;
    private GameManager _gM;
    private float _musicVolume;

    void Awake() {
        _I = this;
    }
    void Start() {
        _gAM = GameAudioManager._I;
        RoomController._onCombatStart += PlayCombat;
        RoomController._onCombatEnd += CombatEnd;
        ChangeVolume();
        _combatSource.Stop();
        GameManager._onVolumeChange += ChangeVolume;
    }

    public void ChangeVolume() {
        _musicVolume = GameManager._gSettings._musicVolume;
        _normalSource.volume = _musicVolume;
        _combatSource.volume = _musicVolume;
        _loopSource.volume = _musicVolume;
        _startScreenSource.volume = _musicVolume;

    }
    void Update() {
        if (!_normalSource.isPlaying && !_isCombat) {
            PlayNormal();
        }
    }
    public void PlayLoopResetApproach() {
        _loopSource.clip = _gAM._approachingLoopReset;
        _normalSource.volume = .33f * _musicVolume;
        _combatSource.volume = .33f * _musicVolume;
        _loopSource.Play();
    }
    public void PlayLoopBreak() {
        _loopSource.clip = _gAM._loopBreak;
        _normalSource.volume = 0 * _musicVolume;
        _combatSource.volume = 0 * _musicVolume;
        _loopSource.Play();

    }

    public void CombatEnd() {
        StopCombat();
    }
    public void PlayBossDie() {
        _loopSource.clip = _gAM._bossDie;
        _loopSource.Play();

    }
    public void PlayLoopReset() {
        _normalSource.volume = _musicVolume;
        _combatSource.volume = _musicVolume;
        _loopSource.clip = _gAM._loopResetSound;
        _loopSource.Play();
    }
    public void PlayCombat() {
        Debug.Log("CombatStart");
        _normalSource.Stop();
        _isCombat = true;
        if (_combatSource.clip == null) _combatSource.clip = MyRandom.GetFromArray<AudioClip>(_gAM._combatSound);
        _combatSource.Play();
    }
    public void StopCombat() {
        Debug.Log("CombatEnd");
        _combatSource.Stop();
        _isCombat = false;
        _normalSource.Play();
    }
    public void PlayNormal() {
        index++;
        if (index == _gAM._defaultSound.Length) index = 0;

        _normalSource.clip = _gAM.GetNormalByIndex(index);
        _normalSource.Play();
    }
}
