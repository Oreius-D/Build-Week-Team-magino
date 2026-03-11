using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SO_UpgradeItems : ScriptableObject
{
    //Nome del potenziamento
    [SerializeField] private string _nameUpgrade;

    [SerializeField] private int _cost;
    [SerializeField] private bool _isUnlocked;
    //[SerializeField] private bool _isConsumable;

    public int Cost => _cost;
    public bool IsUnlocked
    {
        get => _isUnlocked;
        set => _isUnlocked = value;
    }
    //public bool IsConsumable
    //{
    //    get => _isConsumable;
    //    set => _isConsumable = value;
    //}
}