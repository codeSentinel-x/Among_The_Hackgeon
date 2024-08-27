using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TEST : MonoBehaviour
{
    public bool _destroyOnLoad;

    void Awake(){
        if (_destroyOnLoad) Destroy(gameObject);
    }
}
