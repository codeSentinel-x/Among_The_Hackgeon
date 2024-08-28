using System;
using MyUtils.Enums;
using MyUtils.Interfaces;
using UnityEngine;

//TODO //FIXME MAKE THIS CODE MORE FLEXIBLE AND STOP REPEATING SAME FUNCTIONS AGAIN AND AGAIN!!!!!!!  
public class DoorController : MonoBehaviour, IDamageable, IDoor {
    public DoorOpenType _doorType;
    public RoomController _roomToShow;
    public RoomController _tunnelToShow;
    public DoorPosition _pos;
    public bool _openedByDefault;
    public bool _initialized;
    public Action _openDoor;
    public Action _closeDoor;
    public Action _uncloseDoor;
    void Start() {
        // _roomToShow2 = transform.parent.GetComponent<RoomController>();
        // Initialize();
        GetComponent<AudioSource>().volume = GameManager._gSettings._soundsVolume;
    }
    void Close() {
        // GetComponent<SpriteRenderer>().
    }
    public void Initialize() {
        if (_initialized) return;
        _initialized = true;
        if (_doorType != DoorOpenType.BossRoom && _doorType != DoorOpenType.BossRoomDoors) _doorType = UnityEngine.Random.Range(0f, 1f) > 0.5f ? DoorOpenType.OpenOnShoot : DoorOpenType.AlwaysOpen;
        switch (_doorType) {
            case DoorOpenType.AlwaysOpen: {
                    _opened = false;

                    if (_roomToShow._roomType == RoomType.EnemyRoom)
                        _roomToShow._onPlayerEnter += () => {
                            if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                        };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };

                    break;
                }
            case DoorOpenType.OpenOnShoot: {
                    _opened = false;
                    if (_roomToShow._roomType == RoomType.EnemyRoom)
                        _roomToShow._onPlayerEnter += () => {
                            if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                        };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
            case DoorOpenType.BossRoom: {
                    _opened = false;
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };

                    break;
                }
            case DoorOpenType.BossRoomDoors: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };

                    break;
                }
            case DoorOpenType.OpenOnTime: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
            case DoorOpenType.OpenOnButtonClick: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
            case DoorOpenType.OpenOnPlayerStat: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
            case DoorOpenType.OpenOnDefeatEnemy: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
            case DoorOpenType.OpenOnCustomItemHold: {
                    _opened = false;
                    _roomToShow._onPlayerEnter += () => {
                        if (!_roomToShow._wasInvoked) _closeDoor?.Invoke();
                    };
                    _closeDoor = () => {
                        //TODO
                    };
                    _uncloseDoor = () => {
                        //todo
                    };
                    _openDoor = () => {
                        //todo
                    };
                    break;
                }
        }
    }
    public Collider2D _col;
    public int _stage;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = false;
    private bool _hidden;
    public void InitDoor() {
        _gMD = AssetManager._I;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _col.isTrigger = false;
        if (_doorType == DoorOpenType.OpenOnShoot) {
            _stage = _pos switch {
                _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal.Length - 1,
                _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight.Length - 1,
                _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal.Length - 1,
                _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft.Length - 1,
                _ => 0,
            };
            _renderer.sprite = _pos switch {
                _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[_stage],
                _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[_stage],
                _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[_stage],
                _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[_stage],
                _ => _gMD._openedDoorSprite[0],
            };
        } else {
            _renderer.sprite = _pos switch {
                _ when _pos == DoorPosition.Up => _gMD._closedDoorSprite[0],
                _ when _pos == DoorPosition.Right => _gMD._closedDoorSprite[1],
                _ when _pos == DoorPosition.Down => _gMD._closedDoorSprite[2],
                _ when _pos == DoorPosition.Left => _gMD._closedDoorSprite[3],
                _ => _gMD._closedDoorSprite[0],
            };

        }

        _discovered = false;
        _opened = false;
    }
    public void CloseDoor() {
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
        if (_doorType == DoorOpenType.OpenOnShoot) {
            if (_stage == 0) {
                OpenDoor();
                AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position);
                return;
            } else {
                _stage--;
                _renderer.sprite = _pos switch {
                    _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[_stage],
                    _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[_stage],
                    _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[_stage],
                    _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[_stage],
                    _ => _gMD._openedDoorSprite[0],
                };
            }
            return;
        } else {
            OpenDoor();
            AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position);
            return;

        }



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
        if (d._tunnelToShow._maskTransform != null) d._tunnelToShow._maskTransform.gameObject.SetActive(false);
        d._tunnelToShow.ShowRoom();
        d._roomToShow.ShowRoom();
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
    }
}
public enum DoorPosition {
    Left, Right, Up, Down
}
//-----------------------------------------------------------------------------------------------------------------------------------

[RequireComponent(typeof(SpriteRenderer))]
public class DoorOnShoot : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    public int _stage;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = false;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = AssetManager._I;
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
        _opened = false;
        _discovered = false;
        GetComponent<DoorController>()._roomToShow._onRoomClear += (x) => { if (x == GetComponent<DoorController>()._roomToShow) UncloseDoor(); };
    }
    public void CloseDoor() {
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
        if (d._tunnelToShow._maskTransform != null) d._tunnelToShow._maskTransform.gameObject.SetActive(false);
        d._tunnelToShow.ShowRoom();
        d._roomToShow.ShowRoom();
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
    }

    public void Damage(float v) {
        throw new System.NotImplementedException();
    }
}
public class DoorOnBlank : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = false;
    public DoorPosition _pos;
    private bool _hidden;


    public void CloseDoor() {
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

    void Start() {
        _gMD = AssetManager._I;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        GetComponent<DoorController>()._roomToShow._onRoomClear += (RoomController x) => { if (x == GetComponent<DoorController>()._roomToShow) UncloseDoor(); };
        _col.isTrigger = false;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[^1],
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[^1],
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[^1],
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[^1],
            _ => _gMD._openedDoorSprite[0],
        };
        _opened = false;
        _discovered = false;
    }
    public void Show() {
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[2],
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[2],
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[2],
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[2],
            _ => _gMD._openedDoorSprite[0],
        };
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
        if (d._tunnelToShow._maskTransform != null) d._tunnelToShow._maskTransform.gameObject.SetActive(false);
        d._tunnelToShow.ShowRoom();
        d._roomToShow.ShowRoom();
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
    }

    public void Damage(float v) {
        throw new System.NotImplementedException();
    }
}

public class VisibleDoor : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = AssetManager._I;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        GetComponent<DoorController>()._roomToShow._onRoomClear += (RoomController x) => { if (x == GetComponent<DoorController>()._roomToShow) UncloseDoor(); };
        _col.isTrigger = false;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._closedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._closedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._closedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._closedDoorSprite[3],
            _ => _gMD._closedDoorSprite[0],
        };
        _opened = false;
        _discovered = false;
    }
    public void CloseDoor() {
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
        if (d._tunnelToShow._maskTransform != null) d._tunnelToShow._maskTransform.gameObject.SetActive(false);
        d._tunnelToShow.ShowRoom();
        d._roomToShow.ShowRoom();
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };

        // _renderer.enabled = false;
        // throw new System.NotImplementedException();
    }

    public void Damage(float v) {
        if (_opened || _hidden) return;
        OpenDoor(); AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position); return;

    }
}
public class BossEnterDoor : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = AssetManager._I;
        _col = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        // GetComponent<DoorController>()._roomToShow._onRoomClear += (x) => { if (x == GetComponent<DoorController>()._roomToShow) ShowDoor(); };
        _col.isTrigger = false;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._closedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._closedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._closedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._closedDoorSprite[3],
            _ => _gMD._closedDoorSprite[0],
        };
        _opened = false;
        _discovered = false;
    }
    public void CloseDoor() {
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
        if (d._tunnelToShow != null) {
            if (d._tunnelToShow._maskTransform != null) d._tunnelToShow._maskTransform.gameObject.SetActive(false);
            d._tunnelToShow.ShowRoom();
            d._roomToShow.ShowRoom();

        }
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };

        // _renderer.enabled = false;
        // throw new System.NotImplementedException();
    }

    public void Damage(float v) {
        if (!PlayerController._hasKey) return;
        if (_opened || _hidden) return;
        OpenDoor(); AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position); return;

    }
}
[RequireComponent(typeof(SpriteRenderer))]
public class BossDoorOnShoot : MonoBehaviour, IDoor, IDamageable {

    public Collider2D _col;
    public int _stage;
    private AssetManager _gMD;
    private SpriteRenderer _renderer;
    public bool _opened;
    private bool _discovered = false;
    public DoorPosition _pos;
    private bool _hidden;
    void Start() {
        _gMD = AssetManager._I;
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
        _opened = false;
        _discovered = false;
        GetComponent<DoorController>()._roomToShow._onRoomClear += (x) => { if (x == GetComponent<DoorController>()._roomToShow) UncloseDoor(); };
    }
    public void CloseDoor() {
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
        if (_stage == 0) { OpenDoor(); AudioManager._I.PlaySoundEffect(AudioType.DoorOpen, transform.position); return; }
        _stage--;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Right => _gMD._destroyableDoorSpritesVerticalRight[_stage],
            _ when _pos == DoorPosition.Down => _gMD._destroyableDoorSpritesHorizontal[_stage],
            _ when _pos == DoorPosition.Left => _gMD._destroyableDoorSpritesVerticalLeft[_stage],
            _ => _gMD._openedDoorSprite[0],
        };



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
        d._roomToShow.ShowRoom();
    }

    public void UncloseDoor() {
        _hidden = false;
        if (!_discovered) return;
        _opened = true;
        _col.isTrigger = true;
        _renderer.sprite = _pos switch {
            _ when _pos == DoorPosition.Up => _gMD._openedDoorSprite[0],
            _ when _pos == DoorPosition.Right => _gMD._openedDoorSprite[1],
            _ when _pos == DoorPosition.Down => _gMD._openedDoorSprite[2],
            _ when _pos == DoorPosition.Left => _gMD._openedDoorSprite[3],
            _ => _gMD._openedDoorSprite[0],
        };
    }
}
