using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour {

    private ParticleSystem _particle;
    void Start() {
        _particle = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (!_particle.isEmitting) Destroy(this.gameObject);
    }
}
