using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public GameObject _tooltipCanvas;

    void Awake(){
        DontDestroyOnLoad(_tooltipCanvas);
    }
}
