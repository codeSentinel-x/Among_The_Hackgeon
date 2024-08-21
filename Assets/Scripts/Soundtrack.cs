using MyUtils.Functions;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour {
    public static Soundtrack _I;
    public int index;
    public AudioClip[] _defaultSound;
    public AudioClip[] _combatSound;
    private AudioSource _source;
    void Awake() {
        _I = this;
    }
    void Start() {
        _source = GetComponent<AudioSource>();
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
    }
}
