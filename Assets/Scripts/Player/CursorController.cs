using UnityEngine;

public class CursorController : MonoBehaviour {

    void Start() {
        Cursor.visible = false;
    }

    void Update() {
        var v = Camera.main.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        transform.position = new(v.x, v.y, -1);
    }
}
