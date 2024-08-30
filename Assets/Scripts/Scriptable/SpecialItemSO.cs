using MyUtils.Structs;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Item")]
public class SpecialItemSO : ScriptableObject {
    public Sprite _sprite;
    public StatChangeObject[] _statsToChange;
    public string _name;
    public string _shortDesc;
    public bool _isStackable;
    public int _maxStack;
    public int _stackSize;

}
