using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuovoUpgrade" , menuName = "Shop")]
public class SO_UpgradeItems : ScriptableObject
{
    //Nome del potenziamento
    [SerializeField] private string _nameUpgrade;

    public int _cost;
    public bool _isUnlocked;
}
