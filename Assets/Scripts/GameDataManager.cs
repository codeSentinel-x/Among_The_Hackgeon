using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _I;
    public Sprite[] _destroyableDoorSprites;

    void Awake() {
        _I = this;
    }
}
