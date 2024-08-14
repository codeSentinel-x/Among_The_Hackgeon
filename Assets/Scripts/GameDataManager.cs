using NUnit.Framework;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _I;
    public Sprite[] _destroyableDoorSprites;
    public Sprite _closedHorizontalDoorSprite;
    public Sprite _closedVerticalDoorSprite;
    public Transform _enemyPref;
    public Sprite _bulletSprite;
    void Awake() {
        _I = this;
    }
    public static WeaponSO LoadByName(string name) => Resources.Load<WeaponSO>($"Weapons/{name}");

}
