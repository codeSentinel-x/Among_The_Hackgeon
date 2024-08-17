using NUnit.Framework;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _I;
    public Sprite[] _destroyableDoorSpritesHorizontal;
    public Sprite[] _destroyableDoorSpritesVerticalLeft;
    public Sprite[] _destroyableDoorSpritesVerticalRight;
    public Sprite[] _closedDoorSprite;
    public Sprite[] _openedDoorSprite; //0 - up; 1- right, 2 - down; 3 - left
    public Transform _enemyPref;
    public Sprite _bulletSprite;
    public Sprite _blankSprite;
    void Awake() {
        _I = this;
    }
    public static WeaponSO LoadWeaponByName(string name) => Resources.Load<WeaponSO>($"Weapons/{name}");
    public static SpecialItemSO LoadItemByName(string name) => Resources.Load<SpecialItemSO>($"Items/{name}");

}
