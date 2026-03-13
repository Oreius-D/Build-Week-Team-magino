using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SO_UpgradeItems : ScriptableObject
{
    //Nome del potenziamento
    [SerializeField] private string nameUpgrade;

    [SerializeField] private int cost;
    [SerializeField] private bool isUnlocked;
    //[SerializeField] private bool _isConsumable;

    public int Cost => cost;
    public bool IsUnlocked
    {
        get => isUnlocked;
        set => isUnlocked = value;
    }
    //public bool IsConsumable
    //{
    //    get => _isConsumable;
    //    set => _isConsumable = value;
    //}
}