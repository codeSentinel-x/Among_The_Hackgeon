using System;
using MyUtils.Enums;
using MyUtils.Interfaces;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public DoorOpenType _doorType;
    public RoomController _roomToShow1;
    [HideInInspector] public RoomController _roomToShow2;
    public DoorPosition _pos;
    public bool _openedByDefault;
    void Start() {
        // _roomToShow2 = transform.parent.GetComponent<RoomController>();
        // Initialize();
    }
    void Close() {
        // GetComponent<SpriteRenderer>().
    }
    public void Initialize() {
        switch (_doorType) {
            case DoorOpenType.AlwaysOpen: {
                    var c = gameObject.AddComponent<AlwaysOpenDoor>();
                    c._pos = _pos;
                    c._opened = _openedByDefault;
                    Debug.Log(_roomToShow2.name);
                    if (_roomToShow2._roomType == RoomType.EnemyRoom) _roomToShow2._onPlayerEnter += () => { if (!_roomToShow2._wasInvoked) c.CloseDoor(); };
                    break;
                }
            case DoorOpenType.OpenOnShoot: {
                    var c = gameObject.AddComponent<DoorOnShoot>();
                    c._pos = _pos;
                    c._opened = _openedByDefault;
                    Debug.Log(_roomToShow2.name);
                    if (_roomToShow2._roomType == RoomType.EnemyRoom) _roomToShow2._onPlayerEnter += () => { if (!_roomToShow2._wasInvoked) c.CloseDoor(); };
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
public enum DoorPosition {
    Left, Right, Up, Down
}
[RequireComponent(typeof(SpriteRenderer))]
public class DoorOnShoot : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    public int _stage;
    private GameDataManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = true;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = GameDataManager._I;
        _stage = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal.Length - 1,
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight.Length - 1,
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal.Length - 1,
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft.Length - 1,
            _ => 0,
        };
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[_stage],
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[_stage],
            _ => _gMD._openedDoorSprite[0],
        };
        _col.isTrigger = false;
        RoomController._onRoomClear += OpenDoor;
    }
    public void CloseDoor() {
        //TODO
        _hidden = true;
        if (_opened) {
            _col.isTrigger = false;
            // _renderer.enabled = true;
            _renderer.sprite = _pos switch {
                _ when _pos == DoorPosition.Up => _gMD._closedDoorSprite[0],
                _ when _pos == DoorPosition.Right => _gMD._closedDoorSprite[1],
                _ when _pos == DoorPosition.Down => _gMD._closedDoorSprite[2],
                _ when _pos == DoorPosition.Left => _gMD._closedDoorSprite[3],
                _ => _gMD._closedDoorSprite[0],
            };
        }
    }

    public void Damage(float v) {
        if (_opened || _hidden) return;
        if (_stage == 0) { OpenDoor(); return; }
        _stage--;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[_stage],
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[_stage],
            _ => _gMD._openedDoorSprite[0],
        };



    }

    public void HideDoor() {
        //TODO g
        throw new System.NotImplementedException();
    }

    public void OpenDoor() {
        _opened = true;
        _discovered = true; ;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
        var d = GetComponent<DoorController>();
        if (d._roomToShow1._maskTransform != null) d._roomToShow1._maskTransform.gameObject.SetActive(false);
        d._roomToShow1?.ShowRoom();
        d._roomToShow2?.ShowRoom();
        //TODO
    }

    public void ShowDoor() {
        //TODO
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        // _renderer.enabled = false;
        // throw new System.NotImplementedException();
    }
}
public class AlwaysOpenDoor : MonoBehaviour, IDoor {

    public Collider2D _col;
    private GameDataManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = true;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = GameDataManager._I;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        RoomController._onRoomClear += OpenDoor;
    }
    public void CloseDoor() {
        //TODO
        _hidden = true;
        if (_opened) {
            _col.isTrigger = false;
            // _renderer.enabled = true;
            _renderer.sprite = _pos switch {
                _ when _pos == DoorPosition.Up => _gMD._closedDoorSprite[0],
                _ when _pos == DoorPosition.Right => _gMD._closedDoorSprite[1],
                _ when _pos == DoorPosition.Down => _gMD._closedDoorSprite[2],
                _ when _pos == DoorPosition.Left => _gMD._closedDoorSprite[3],
                _ => _gMD._closedDoorSprite[0],
            };
        }
    }


    public void HideDoor() {
        //TODO g
        throw new System.NotImplementedException();
    }

    public void OpenDoor() {
        _opened = true;
        _discovered = true; ;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
        var d = GetComponent<DoorController>();
        d._roomToShow1?.ShowRoom();
        d._roomToShow2?.ShowRoom();
        //TODO
    }

    public void ShowDoor() {
        //TODO
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        // _renderer.enabled = false;
        // throw new System.NotImplementedException();
    }
}