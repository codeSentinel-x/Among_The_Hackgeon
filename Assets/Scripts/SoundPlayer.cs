using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    AudioSource _source;
    public void Play(AudioClip clip) {
        _source = GetComponent<AudioSource>();
        _source.volume = GameManager._gSettings._soundsVolume;
        _source.clip = clip;
        _source.Play();
    }
    void Update() {
        if (_source.isPlaying) Destroy(gameObject);
        if (_source == null) return;
    }
}
