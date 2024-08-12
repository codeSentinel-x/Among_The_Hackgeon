using UnityEngine;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _I;
    public Sprite[] _destroyableDoorSprites;
    public Transform _enemyPref;
    void Awake() {
        _I = this;
    }
}
