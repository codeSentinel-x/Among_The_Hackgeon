using UnityEngine;

public class TEST : MonoBehaviour {
    public bool _destroyOnLoad;
    public bool _disableOnAwake;
    public bool _enableOnAwake;
    public bool _enableSpriteOnAwake;
    public bool _disableSpriteOnAwake;
    void Awake() {
        if (_disableOnAwake) gameObject.SetActive(false);
        else if (_enableOnAwake) gameObject.SetActive(true);
        if (_destroyOnLoad) Destroy(gameObject);
        if (_enableSpriteOnAwake) GetComponent<SpriteRenderer>().enabled = true;
        else if (_disableSpriteOnAwake) GetComponent<SpriteRenderer>().enabled = false;
    }
    void Start() {
        InputManager._I.GetKeyPressed(KeyBindType.MoveLeft).AddListener(() => Debug.Log("Move leftPressed"));
    }
}
