using System.Collections;
using System.Collections.Generic;
using MyUtils.Interfaces;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public BoxCollider2D _collider;
    public List<IInteractable> _objectsInRange;
    void Awake(){
        _objectsInRange = new();
        _collider = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.TryGetComponent<IInteractable>(out var i)) {
            if (_objectsInRange.Contains(i)) return;
            _objectsInRange.Add(i);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.TryGetComponent<IInteractable>(out var i)) {
            if (!_objectsInRange.Contains(i)) return;
            _objectsInRange.Remove(i);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            Interact();
        }
    }
    public void Interact(){
        if(_objectsInRange.Count >0){
            _objectsInRange[^1].Interact();
            Debug.Log($"Interacting with {_objectsInRange[^1]}");
        }
    }
}
