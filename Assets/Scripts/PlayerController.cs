using Cinemachine;
using UnityEngine;
using MyUtils.Classes;



public class PlayerController : MonoBehaviour {

    public static PlayerController _I;
    public PlayerData _data;
    public Camera _cam;
    public CinemachineConfiner2D _confirmed;
    public RoomController _currentRoom;
    public ItemPickUp _currentItemInRange;
    void Awake() {
        _I = this;

    }
    void Start() {
        _data._mS.InvokeOnChangeAction();
        _data._dP.InvokeOnChangeAction();
        _data._dD.InvokeOnChangeAction();
        _data._sRD.InvokeOnChangeAction();
        _data._sRPSM.InvokeOnChangeAction();
        _data._mSt.InvokeOnChangeAction();
        _data._dSU.InvokeOnChangeAction();
        _data._iAD.InvokeOnChangeAction();
        _data._mH.InvokeOnChangeAction();
        _data._dI.InvokeOnChangeAction();
        _data._dR.InvokeOnChangeAction();
        _data._rSM.InvokeOnChangeAction();
        _data._bSM.InvokeOnChangeAction();
        _data._sDM.InvokeOnChangeAction();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (_currentItemInRange != null) {
                switch (_currentItemInRange._itemType) {
                    case ItemType.Ammo: {
                            GetComponent<PlayerCombat>().AddAmmo();
                            break;
                        }
                    case ItemType.Healing: {
                            GetComponent<PlayerCombat>().RestoreHealth();
                            break;
                        }
                    case ItemType.Weapon: {
                            GetComponent<PlayerCombat>().AddWeapon(GameDataManager.LoadWeaponByName(_currentItemInRange._name));
                            break;
                        }
                    case ItemType.Special: {
                            _currentItemInRange.GetComponent<SpecialItemPickUp>().Apply();
                            break;
                        }
                    case ItemType.Blank: {
                            GetComponent<PlayerCombat>().AddBlank();
                            break;
                        }

                    default: break;
                }
                Destroy(_currentItemInRange.gameObject);
            }
        }
    }
}
