using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float _maxX;
    public float _minX;
    public float _maxY;
    public float _minY;
    public Transform _cameraTransform;
    public float _mouseOffset;
    public float _speed;
    public bool _followMouse;

    public void Update(){
        _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, M)
    }
}
