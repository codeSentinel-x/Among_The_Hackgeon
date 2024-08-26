using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Torch : MonoBehaviour {

    public float _maxLightIntensity;
    public float _maxLightOuter;
    public int _steps;
    public float _stepInterval;
    private float intensity;
    private float outer;
    public bool _test;
    void Awake() {
        intensity = 0;
        outer = 0;
        if (_test) StartCoroutine(LightCycle());
    }
    public IEnumerator LightCycle() {
        for (int i = 0; i < 10; i++) {
            StartLightsUp();
            yield return new WaitForSeconds(Random.Range(2, 4));
            StartLightsDown();
            yield return new WaitForSeconds(Random.Range(2, 4));

        }
    }
    public void StartLightsUp() {
        StopCoroutine(LightsDown());
        StartCoroutine(LightsUp());
    }
    public void StartLightsDown() {
        StopCoroutine(LightsUp());
        StartCoroutine(LightsDown());
    }
    public IEnumerator LightsUp() {
        // StopCoroutine(LightsDown());
        var intensityStep = _maxLightIntensity / _steps;
        var outerStep = _maxLightOuter / _steps;
        for (int i = 0; i < _steps; i++) {
            intensity += intensityStep;
            outer += outerStep;
            foreach (var l in GetComponentsInChildren<Light2D>()) {
                l.intensity = intensity;
                l.pointLightOuterRadius = outer; ;
            }
            yield return new WaitForSeconds(_stepInterval);

        }
    }
    public IEnumerator LightsDown() {
        var intensityStep = _maxLightIntensity / _steps;
        var outerStep = _maxLightOuter / _steps;
        for (int i = 0; i < _steps; i++) {
            intensity -= intensityStep;
            outer -= outerStep;
            foreach (var l in GetComponentsInChildren<Light2D>()) {
                l.intensity = intensity;
                l.pointLightOuterRadius = outer; ;
            }
            yield return new WaitForSeconds(_stepInterval);

        }
    }
}
