using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using MyUtils.Classes;



public class PlayerController : MonoBehaviour {

    public static PlayerController _I;
    public PlayerData _data;
    public Camera _cam;
    public CinemachineConfiner2D _confirmed;
    public RoomController _currentRoom;
    void Awake() {
        _I = this;

    }
    void Start() {
        _data._movementSpeed.InvokeOnChangeAction();
        _data._dashPower.InvokeOnChangeAction();
        _data._dashDuration.InvokeOnChangeAction();
        _data._staminaRegenerationDelay.InvokeOnChangeAction();
        _data._stamRegPerSecMult.InvokeOnChangeAction();
        _data._maxStamina.InvokeOnChangeAction();
        _data._dashStaminaUsage.InvokeOnChangeAction();
        _data._invincibleAfterDash.InvokeOnChangeAction();
        _data._maxHealth.InvokeOnChangeAction();
        _data._damageIgnore.InvokeOnChangeAction();
        _data._damageReduction.InvokeOnChangeAction();
        _data._reloadSpeedMult.InvokeOnChangeAction();
        _data._bulletSpeedMult.InvokeOnChangeAction();
        _data._shootDelayMult.InvokeOnChangeAction();
    }
}
