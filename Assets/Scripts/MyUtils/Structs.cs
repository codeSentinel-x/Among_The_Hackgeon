using System;
using UnityEngine;

namespace MyUtils.Structs {
    [Serializable]
    public struct BulletSetting {
        public Sprite _sprite;
        public int _damage;
        public float _speed;
        public float _maxDist;
    }
}