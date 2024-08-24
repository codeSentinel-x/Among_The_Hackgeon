using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    AudioSource _source;
    public void Play(AudioClip clip) {
        _source = GetComponent<AudioSource>();
        _source.clip = clip;
    }
    void Update() {
        if (_source == null) return;
        if (!_source.isPlaying) Destroy(gameObject);
    }
}
