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
}
