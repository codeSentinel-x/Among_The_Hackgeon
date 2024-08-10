using System.Collections;
using System.Collections.Generic;
using MyUtils.Enums;
using MyUtils.Interfaces;
using UnityEditor.Build.Content;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public DoorOpenType _doorType;
    void Awake() {
        Initialize();
    }
    public void Initialize() {
        switch (_doorType) {
            case DoorOpenType.OpenOnShoot: {
                    gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnBlank: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnDestroyAllItemOfType: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnTime: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnButtonClick: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnPlayerStat: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnDefeatEnemy: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
            case DoorOpenType.OpenOnCustomItemHold: {
                    //TODO this
                    //gameObject.AddComponent<DoorOnShoot>();
                    break;
                }
        }
    }
}
public class DoorBasic : MonoBehaviour {
    public DoorState state;

}
[RequireComponent(typeof(SpriteRenderer))]
public class DoorOnShoot : MonoBehaviour, IDoor, IDamageable {
    public Collider2D _col;
    public int _stage;
    private GameDataManager _gMD;
    private SpriteRenderer _renderer;
    private bool _opened;
    void Start() {
        _gMD = GameDataManager._I;
        _stage = _gMD._destroyableDoorSprites.Length;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void CloseDoor() {
        //TODO
        throw new System.NotImplementedException();
    }

    public void Damage(int v) {
        if (_opened) return;
        if (_stage == 0) { OpenDoor(); return; }
        _stage--;
        _renderer.sprite = _gMD._destroyableDoorSprites[_stage];


    }

    public void HideDoor() {
        //TODO
        throw new System.NotImplementedException();
    }

    public void OpenDoor() {
        _opened = true;
        _col.isTrigger = true;
        _renderer.enabled = false;
        //TODO
    }

    public void ShowDoor() {
        //TODO
        throw new System.NotImplementedException();
    }
}