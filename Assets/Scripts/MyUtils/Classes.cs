using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


namespace MyUtils.Classes {
    [Serializable]
    public class PlayerData {

    }
    [Serializable]
    public class PlayerStat {

        public Action<float> _OnStatValueChanged;
        [SerializeField] float _baseValue;
        [SerializeField][TextArea] string _description;
        private readonly List<float> _modifiers = new();
        private readonly List<float> _multipliers = new();

        // public;/
        public string GetDescription() => _description;
        public float GetBaseValue() => _baseValue;

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
        public void ChangeBaseValue(float newValue) {
            _baseValue = newValue;
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
        public void InvokeOnChangeAction() {
            _OnStatValueChanged?.Invoke(GetValue());
        }
    }

    [Serializable]
    public class MyGrid {
        public MyGrid(int2 gS, int2 sP, Sprite defS, Transform parent) {
            _elements = new GameObject[gS.x, gS.y];
            _startPos = sP;
            for (int x = 0; x < _elements.GetLength(0); x++) {
                for (int y = 0; y < _elements.GetLength(1); y++) {
                    var g = new GameObject($"Tile ({x},{y})");
                    SpriteRenderer sR = g.AddComponent<SpriteRenderer>();
                    GridElementHandler gE = g.AddComponent<GridElementHandler>();
                    sR.sprite = defS;
                    if (x == 0 || x == gS.x - 1 || y == 0 || y == gS.y - 1) GenerateWall(gE, sR, x, y);
                    else GenerateFloor(gE, sR, x, y);
                    g.transform.position = new(x, y);
                    g.transform.parent = parent;

                    _elements[x, y] = g;
                }
            }
        }
        void GenerateWall(GridElementHandler gE, SpriteRenderer sR, int x, int y) {
            sR.color = new((float)x / 100, (float)y / 100, (float)(x + y) / 100, 1);
            gE.content = new(x, y, "Wall", false, new());
        }
        void GenerateFloor(GridElementHandler gE, SpriteRenderer sR, int x, int y) {
            sR.color = new((float)x / 20, (float)y / 20, (float)(x + y) / 20, 1);
            gE.content = new(x, y, "Floor", true, new());
        }
        public GameObject[,] _elements;
        public int2 _startPos;
    }

    [Serializable]
    public class GridElement {
        public string _name;
        public bool _isWalkable;
        public GridElement(int x, int y, string n, bool isW, Content c) {
            _pos = new(x, y);
            _content = c;
            _name = n;
            _isWalkable = isW;
        }
        public int2 _pos;
        public Content _content;
    }
    [Serializable]
    public class Content {
        public string _name;
    }


}
