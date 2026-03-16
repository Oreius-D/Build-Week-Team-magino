using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SO_UpgradeItems : ScriptableObject
{
    //Nome del potenziamento
    [SerializeField] private string nameUpgrade;
    private string id;
    [SerializeField] private int cost;
    private bool isUnlocked;
    //[SerializeField] private bool _isConsumable;
    public string NameUpgrade
    {
        get => nameUpgrade;
        set => nameUpgrade = value;
    }
    public string ID
    {
        get => id;
        set => id = value;
    }

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