using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    //private int coins;
    //public int Coins
    //{ get => coins; set => coins = value;}
    private int _maxConsumables = 3;
    public SaveData saveData = new SaveData();
    public void AddPassive(SO_PassiveUpgrade upgrade)
    {
        //if (!_passives.Contains(upgrade))
        //{
            
        //    _passives.Add(upgrade);
        //    
        //}
        if (!saveData.PassiveID.Contains(upgrade))
        {
            saveData.PassiveID.Add(upgrade);
        }
        Debug.Log($"[Inventario] {upgrade.name} aggiunto allo zaino");
            

            
    }

    public void AddConsumable(SO_ConsumableUpgrade upgrade)
    {
        
        if (saveData.ConsumableID.Count < _maxConsumables)
        {
            saveData.ConsumableID.Add(upgrade);
            Debug.Log($"[Inventario] {upgrade.name} aggiunto. Spazio : {saveData.ConsumableID.Count}/{_maxConsumables}");
        }
        else
        {
            Debug.Log($"[Inventario] Inventario pieno");
        }
    }

    public bool CanAdd()
    {
        return saveData.ConsumableID.Count < _maxConsumables;
    }

    public bool HaveConsumable(SO_ConsumableUpgrade consumableUpgrade)
    {
        return saveData.ConsumableID.Contains(consumableUpgrade);
    }
    public void UseUpgrade(SO_ConsumableUpgrade upgrade)
    {
        if (saveData != null && saveData.ConsumableID.Contains(upgrade))
        {
            Debug.Log($"{upgrade.name} è stato utilizzato. Spazio : {_maxConsumables - saveData.ConsumableID.Count}");
            saveData.ConsumableID.Remove(upgrade);
            upgrade.IsUnlocked = false;
        }
        
    }
}
