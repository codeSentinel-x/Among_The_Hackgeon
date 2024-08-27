using UnityEngine;

public class TEST : MonoBehaviour
{
    public bool _destroyOnLoad;

    void Awake(){
        if (_destroyOnLoad) Destroy(gameObject);
    }
}
