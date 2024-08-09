using System.Collections;
using System.Collections.Generic;
using MyUtils.Enums;
using MyUtils.Interfaces;
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
public class DoorOnShoot : MonoBehaviour, IDoor {
    public Collider2D col;

    public void CloseDoor() {
        //TODO
        throw new System.NotImplementedException();
    }

    public void HideDoor() {
        //TODO
        throw new System.NotImplementedException();
    }

    public void OpenDoor() {
        //TODO
        throw new System.NotImplementedException();
    }

    public void ShowDoor() {
        //TODO
        throw new System.NotImplementedException();
    }
}