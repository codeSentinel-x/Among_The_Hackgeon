using System;
using System.Collections.Generic;
using MyUtils.Enums;
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
            if (modifier != 0) _ = _modifiers.Remove(modifier);
            InvokeOnChangeAction();
        }
        public void AddMultiplier(float multiplier) {
            if (multiplier != 0) _multipliers.Add(multiplier);
            InvokeOnChangeAction();
        }
        public void RemoveMultiplier(float multiplier) {
            if (multiplier != 0) _ = _multipliers.Remove(multiplier);
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
    [Serializable]
    public class Item {
        public SpecialItemSO _itemBase;
        public int _amount;
        public int _maxAmount;

        public static Item CreateNewItemFromItem(Item item) {
            throw new NotImplementedException();
        }
    }
    [Serializable]
    public class KeyBindData {
        public Dictionary<KeyBindType, KeyBind> _keyBinds = new();
    }
    [Serializable]
    public class KeyBind {
        public KeyCode _key;
        // public Action _onKeyAction;
    }
    [Serializable]
    public class KeyBindArrayElement {
        public KeyBindType _type;
        public KeyBind _keyBind;
    }
    [System.Serializable]
    public class Inventory {
        public Action OnInventorySizeChange;
        public int inventorySize;
        public List<Item> itemList = new();
        public Inventory_UI inventoryUI;
        public Inventory(Inventory_UI inv, int invSize) //Inventory setup
        {
            itemList ??= new();     //this is if(itemList == null){ itemList = new List<Item>()} but written like a pro;
            inventoryUI = inv;
            inventorySize = invSize;
        }


        public bool AddItem(Item item, bool stackWithOther = true) {
            if (itemList.Count == inventorySize) return false;
            if (!item._itemBase._isStackable || !stackWithOther) {
                GetEmptySlot().AddItem(item);
                itemList.Add(item);
                return true;
            } else {
                int amountToAdd = item._amount;
                int amountLeft = amountToAdd;
                foreach (Item it in itemList) {
                    if (it._itemBase == item._itemBase) {
                        if (it._amount != it._maxAmount) {

                            it._amount = CheckAmountToAdd(it, amountToAdd, out int aLeft);
                            InventorySlot slot = FindSlotWithItem(it);
                            if (slot != null) {
                                slot.RefreshItemAmount(it._amount);
                            } else {
                                Debug.Log("Slot is null");
                                continue;
                            }
                            amountLeft = aLeft;
                            if (amountLeft == 0) break;
                        }
                    }
                }
                if (amountLeft > 0) {
                    item._amount = amountLeft;
                    AddItem(item, false);
                }
                return true;
            }
        }
        public void RemoveItem(Item item) {
            // TODO remove item from inventory
            throw new System.NotImplementedException();
        }


        public void IncreaseInventoryCapacity(int amount) {
            inventorySize += amount;
            OnInventorySizeChange?.Invoke();
        }
        public void DecreesInventoryCapacity(int amount) {
            inventorySize -= amount;
            if (inventorySize <= 0) inventorySize = 1;
            OnInventorySizeChange?.Invoke();
        }

        public bool IsEnoughInInventory(Item item, int amountToCheck) {
            return GetItemInInventoryAmount(item) <= amountToCheck;
        }
        public int GetItemInInventoryAmount(Item item) {
            int amount = 0;
            foreach (InventorySlot slot in FindSlotsWIthItem(item)) {
                amount += slot._item._amount;
            }
            return amount;
        }

        private List<InventorySlot> FindSlotsWIthItem(Item item) {
            List<InventorySlot> slots = new();
            foreach (InventorySlot slot in inventoryUI._slots) {
                if (slot._item == item) {
                    slots.Add(slot);
                }
            }
            return slots;
        }

        public bool AddItemAmount(Item item, out int overInventoryAmount) {
            //TODO Add Item Amount
            throw new System.NotImplementedException();
        }
        public int CheckAmountToAdd(Item item, int a, out int overStackAmount) {
            if (!item._itemBase._isStackable) {
                overStackAmount = a;
                return 1;
            }

            int amount = item._amount;
            int _maxStack = item._itemBase._stackSize;
            int control = amount + a;

            if (control > _maxStack) {
                overStackAmount = control - _maxStack;
                amount = _maxStack;
            } else {
                amount += a;
                overStackAmount = 0;
            }
            // Debug.Log("overStackAmount = " + overStackAmount);
            // Debug.Log("amount = " + amount);
            return amount;

        }
        public void ChangeItemAmount(Item item, int a) {
            //TODO change item amount
        }
        public void RemoveItemAmount(int a, out int additionalAmount) {
            //TODO Remove item Amount
            throw new NotImplementedException();
        }

        public void RefreshItemList() {
            itemList = new();
            foreach (InventorySlot slot in inventoryUI._slots) {
                if (!slot._isEmpty) itemList.Add(slot._item);
            }
        }

        public int GetSpaceInInventory() {
            return inventorySize - itemList.Count;
        }
        private List<InventorySlot> FindSlotsWithItemOfType(SpecialItemSO _itemBase) {
            List<InventorySlot> slots = new();
            foreach (InventorySlot slot in inventoryUI._slots) {
                if (slot._item._itemBase == _itemBase) slots.Add(slot);
            }
            return slots;
        }
        private InventorySlot FindSlotWithItem(Item it) {
            foreach (InventorySlot slot in inventoryUI._slots) {
                if (slot._isEmpty) continue;
                if (slot._item == it) return slot;
            }
            return null;
        }
        public InventorySlot GetEmptySlot() {
            foreach (InventorySlot invSlot in inventoryUI._slots) {
                if (invSlot._isEmpty) return invSlot;
            }
            return null;
        }
        public List<InventorySlot> GetEmptySlots() {
            List<InventorySlot> slots = new();
            foreach (InventorySlot slot in inventoryUI._slots) {
                if (slot._isEmpty) slots.Add(slot);
            }
            return slots;
        }


        public bool HaveEmptySLot() {
            throw new NotImplementedException();
        }
        public Item FindItem(Item item) {
            foreach (Item it in itemList) {
                if (it == item) return it;
            }
            return null;
        }
        public List<Item> GetItemList() {
            return itemList;
        }
    }



}
