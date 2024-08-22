using MyUtils.Functions;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
    public static Soundtrack _I;
    public int index;
    public AudioClip[] _defaultSound;
    public AudioClip[] _combatSound;
    public AudioSource _normalSource;
    public AudioSource _loopSource;
    public AudioSource _combatSource;
    public bool _isCombat;
    void Awake() {
        _I = this;
    }
    void Start() {
        RoomController._onCombatStart += PlayCombat;
        RoomController._onCombatEnd += () => { StopCombat(); PlayNormal(); };
        _normalSource.volume = GameManager._gSettings._musicVolume;
        _combatSource.volume = GameManager._gSettings._musicVolume;
        _loopSource.volume = GameManager._gSettings._musicVolume; 
        _combatSource.Stop();
    }

    void Update() {
        if (!_normalSource.isPlaying && !_isCombat) {
            PlayNormal();
        }
    }
    public void PlayLoopResetApproach() {
        _loopSource.clip = GameDataManager._I._approachingLoopReset;
        _normalSource.volume = .33f * GameManager._gSettings._musicVolume;
        _combatSource.volume = .33f * GameManager._gSettings._musicVolume;
        _loopSource.Play();
    }
    public void PlayLoopReset() {
        _normalSource.volume = GameManager._gSettings._musicVolume;
        _combatSource.volume = GameManager._gSettings._musicVolume;
        _loopSource.clip = GameDataManager._I._loopResetSound;
        _loopSource.Play();
    }
    public void PlayCombat() {
        _normalSource.Stop();
        _isCombat = true;
        _combatSource.clip = MyRandom.GetFromArray<AudioClip>(_combatSound);
        _combatSource.Play();
    }
    public void StopCombat() {
        _combatSource.Stop();
        _isCombat = false;
        _normalSource.Play();
    }
    public void PlayNormal() {
        index++;
        if (index == _defaultSound.Length) index = 0;

        _normalSource.clip = _defaultSound[index];
        _normalSource.Play();
    }
}
