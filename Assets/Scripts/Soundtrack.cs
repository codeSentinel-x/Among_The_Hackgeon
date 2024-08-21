using MyUtils.Functions;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour {
    public static Soundtrack _I;
    public int index;
    public AudioClip[] _defaultSound;
    public AudioClip[] _combatSound;
    private AudioSource _normalSource;
    private AudioSource _combatSource;
    public bool _isCombat;
    void Awake() {
        _I = this;
    }
    void Start() {
        _source = GetComponent<AudioSource>();
        RoomController._onCombatStart += PlayCombat;
        RoomController._onCombatEnd += PlayNormal;
    }

    void Update() {
        if (_source.isPlaying == false) {
            PlayNormal();
        }
    }
    public void PlayCombat() {
        _source.clip = MyRandom.GetFromArray<AudioClip>(_combatSound);
    }
    public void PlayNormal() {
        index++;
        if (index == _defaultSound.Length) index = 0;

        _source.clip = _defaultSound[index];
        _source.Play();
    }
}
