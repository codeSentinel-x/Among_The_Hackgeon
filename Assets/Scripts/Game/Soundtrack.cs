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
    private AudioManager _audioM;
    private GameManager _gameM;
    private float _musicVolume;

    void Awake() {
        _I = this;
    }
    void Start() {
        _audioM = AudioManager._I;
        RoomController._onCombatStart += PlayCombat;
        RoomController._onCombatEnd += CombatEnd;
        ChangeVolume();
        _combatSource.Stop();
        GameManager._onVolumeChange += ChangeVolume;
        DontDestroyOnLoad(gameObject);
        PlayStartScreen();
    }
    public void PlayStartScreen() {
        _combatSource.Stop();
        _normalSource.Stop();
        _loopSource.Stop();
        _startScreenSource.clip = MyRandom.GetFromArray<AudioClip>(_audioM._startScreenMusic);
        _startScreenSource.Play();

    }

    public void ChangeVolume() {
        _musicVolume = GameManager._gSettings._musicVolume;
        _normalSource.volume = _musicVolume;
        _combatSource.volume = _musicVolume;
        _loopSource.volume = _musicVolume;
        _startScreenSource.volume = _musicVolume;

    }
    void Update() {
        if (!_normalSource.isPlaying && !_isCombat && !_startScreenSource.isPlaying) {
            PlayNormal();
        }
    }
    public void PlayLoopResetApproach() {
        _loopSource.clip = _audioM._approachingLoopResetSound;
        _normalSource.volume = .33f * _musicVolume;
        _combatSource.volume = .33f * _musicVolume;
        _loopSource.Play();
    }
    public void PlayLoopBreak() {
        _loopSource.clip = _audioM._loopBreakSound;
        _normalSource.volume = 0 * _musicVolume;
        _combatSource.volume = 0 * _musicVolume;
        _loopSource.Play();

    }

    public void CombatEnd() {
        StopCombat();
    }
    public void PlayLoopReset() {
        _normalSource.volume = _musicVolume;
        _combatSource.volume = _musicVolume;
        _loopSource.clip = _audioM._loopResetSound;
        _loopSource.Play();
    }
    public void PlayCombat() {
        Debug.Log("CombatStart");
        _normalSource.Stop();
        _isCombat = true;
        if (_combatSource.clip == null) _combatSource.clip = MyRandom.GetFromArray<AudioClip>(_audioM._combatMusic);
        _combatSource.Play();
    }
    public void StopCombat() {
        Debug.Log("CombatEnd");
        _isCombat = false;
        _normalSource.Play();
        _combatSource.Stop();
    }
    public void PlayNormal() {
        _startScreenSource.Stop();
        index++;
        if (index == _audioM._defaultMusic.Length) index = 0;

        _normalSource.clip = _audioM.GetNormalByIndex(index);
        _normalSource.Play();
    }
}
