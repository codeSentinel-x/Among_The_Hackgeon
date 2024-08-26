using System;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtils.Classes {
    [Serializable]
    public class PlayerData {
        //Movement
        public PlayerStat _movementSpeed;
        public PlayerStat _dashPower;
        public PlayerStat _dashDuration;
        public PlayerStat _staminaRegenerationDelay;
        public PlayerStat _stamRegPerSecMult;  // stamina regeneration per second multiplier (I am to lazy to time this every time)
        public PlayerStat _maxStamina;
        public PlayerStat _dashStaminaUsage;
        public PlayerStat _invincibleAfterDash;
        //Defense
        public PlayerStat _maxHealth;
        public PlayerStat _damageIgnore;
        public PlayerStat _damageReduction;
        //Offense
        public PlayerStat _reloadSpeedMult;
        public PlayerStat __bulletSpeedMult;
        public PlayerStat _shootDelayMultiplier;
    }
    public class MessagesHolder {
        public string tag;
        public bool isEnabled = false;
        public List<Message> messages = new();
        public int totalCount = 0;
        public float lastOccurrence = 0;
        public MessagesHolder(string t, string fM) {
            tag = t;
            messages = new() { new Message(fM, 0, Time.time) };
        }
        public void AddMessage(string message, bool collapse) {
            if (collapse) {
                bool found = false;
                foreach (var m in messages) {
                    if (m.content == message) {
                        totalCount++;
                        m.count++;
                        m.lastOccurrence = Time.time;
                        lastOccurrence = Time.time;
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    messages.Add(new Message(message, 0, Time.time));
                    totalCount += 1;
                    lastOccurrence = Time.time;
                }
            } else {
                messages.Add(new Message(message, 0, Time.time));
                totalCount += 1;
                lastOccurrence = Time.time;
            }
        }
    }
    public class Message {
        public Message(string c, int i, float l) {
            content = c;
            count = i;
            lastOccurrence = l;
        }
        public string content;
        public int count;
        public float lastOccurrence;
    }
    [Serializable]
    public class PlayerStat {

        public Action<float> _OnStatValueChanged;
        [SerializeField] float _baseValue;
        [SerializeField][TextArea] string _description;
        private readonly List<float> _modifiers = new();
        private readonly List<float> _multipliers = new();

        public string GetDescription() => _description;
        public float GetBaseValue() => _baseValue;
        public void ChangeBaseValue(float newValue) => _baseValue = newValue;
        public void InvokeOnChangeAction() => _OnStatValueChanged?.Invoke(GetValue());


        public float GetValue() {

            float finalValue;
            finalValue = _baseValue;
            _modifiers.ForEach(x => finalValue += x);
            _multipliers.ForEach(y => finalValue *= y);
            return finalValue;

        }
        public float GetModifiers() {
            float value = 0;
            _modifiers.ForEach(x => value += x);
            return value;
        }

        public float GetMultipliers() {
            float value = 1;
            _multipliers.ForEach(x => value *= x);
            return value;
        }
        public void AddModifier(float modifier) {
            if (modifier != 0) _modifiers.Add(modifier);
            InvokeOnChangeAction();
        }
        public void RemoveModifier(float modifier) {
            if (modifier != 0) _modifiers.Remove(modifier);
            InvokeOnChangeAction();
        }
        public void AddMultiplier(float multiplier) {
            if (multiplier != 0) _multipliers.Add(multiplier);
            InvokeOnChangeAction();
        }
        public void RemoveMultiplier(float multiplier) {
            if (multiplier != 0) _multipliers.Remove(multiplier);
            InvokeOnChangeAction();
        }
        public void ClearAllModifiers() {
            _modifiers.Clear();
            InvokeOnChangeAction();
        }
        public void ClearAllMultipliers() {
            _multipliers.Clear();
            InvokeOnChangeAction();
        }
        public void ClearAll() {
            _modifiers.Clear();
            _multipliers.Clear();
            InvokeOnChangeAction();
        }

    }

    [Serializable]
    public class GameSettings {
        public GameSettings(float m, float s, float eS, float eD, float eR, float eH, float iS, float pS, float pD, float pR, float pH, float t, int fR) {
            _musicVolume = m;
            _soundsVolume = s;
            _enemySpeedMultiplier = eS;
            _enemyDamageMultiplier = eD;
            _enemyDamageReductionMultiplier = eR;
            _enemyMaxHealthMultiplier = eH;
            _specialItemSpawnChange = iS;
            _playerSpeedMultiplier = pS;
            _playerDamageMultiplier = pD;
            _playerDamageReductionMultiplier = pR;
            _playerMaxHealthMultiplier = pH;
            _timeMultiplier = t;
            _targetFrameRate = fR;
        }
        [Range(0, 1f)] public float _musicVolume;
        [Range(0, 1f)] public float _soundsVolume;
        [Range(0.1f, 3f)] public float _enemySpeedMultiplier;
        [Range(0.1f, 3f)] public float _enemyDamageMultiplier;
        [Range(0f, 1f)] public float _enemyDamageReductionMultiplier;
        [Range(0.1f, 3f)] public float _enemyMaxHealthMultiplier;
        [Range(0.1f, 3f)] public float _specialItemSpawnChange;
        [Range(0.1f, 3f)] public float _playerSpeedMultiplier;
        [Range(0.1f, 3f)] public float _playerDamageMultiplier;
        [Range(0f, 1f)] public float _playerDamageReductionMultiplier;
        [Range(0.1f, 3f)] public float _playerMaxHealthMultiplier;
        [Range(0.1f, 3f)] public float _timeMultiplier;
        public int _targetFrameRate;
    }
    [Serializable]
    public class Weapon {
        public Weapon(WeaponSO def) {
            _defaultSettings = def;
            _bulletsInMagazine = def._maxBullet;
            _reloadTime = def._reloadTime;
            _allBullets = 10 * _bulletsInMagazine;
        }
        public int _bulletsInMagazine;
        public int _allBullets;
        public float _reloadTime;
        public WeaponSO _defaultSettings;
        public float _nextShoot;

        public void Setup(Transform firePoint, SpriteRenderer spriteR) {
            if (firePoint != null) firePoint.localPosition = _defaultSettings._firePointPos;
            spriteR.sprite = _defaultSettings._sprite;
        }
        public void Reload() {
            if (_allBullets >= _defaultSettings._maxBullet) {
                _bulletsInMagazine = _defaultSettings._maxBullet;
                _allBullets -= _bulletsInMagazine;
            } else {
                _bulletsInMagazine = _allBullets;
                _allBullets = 0;
            }
        }
        public void Shoot(float mult) {
            _nextShoot = Time.time + _defaultSettings._shotDelay * mult;
            _bulletsInMagazine -= 1;
        }
    }
    [Serializable]
    public class PlayerSaveData {
        public PlayerData _data;
        public Vector3 _playerPos;
        public RoomController[] _rooms;
        public int _saveIndex;

    }
    


}
