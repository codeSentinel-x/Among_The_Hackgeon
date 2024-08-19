using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
    public TextMeshProUGUI _display;
    public float _time;
    void Start() {
        _time = 490f;
    }
    void Update() {
        _time -= Time.deltaTime;
        _display.text = _time.ToString("f0");
    }
}
