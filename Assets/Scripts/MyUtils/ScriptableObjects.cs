using System.Collections;
using System.Collections.Generic;
using MyUtils.Structs;
using UnityEngine;

namespace MyUtils.ScriptableObjects {

    [CreateAssetMenu(menuName = "Scriptable/Weapon")]
    public class WeaponSO : ScriptableObject {
        public Sprite _sprite;
        public Vector3 _firePointPos;
        public Transform _bulletPref;
        public BulletSetting _bulletSetting;
    }
}