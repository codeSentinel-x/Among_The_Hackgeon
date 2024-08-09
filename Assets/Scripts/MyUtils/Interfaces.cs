using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtils.Interfaces{
    public interface IDoor{
        public void OpenDoor();
        public void ShowDoor();
        public void CloseDoor();
        public void HideDoor();
    }
    public interface IDamageable{
        public void Damage(int v);
    }
} 
