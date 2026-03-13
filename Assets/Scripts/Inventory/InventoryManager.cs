using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<SO_PassiveUpgrade> _passives = new List<SO_PassiveUpgrade>();
    [SerializeField] private List<SO_ConsumableUpgrade> _consumables = new List<SO_ConsumableUpgrade>();
    private int _maxConsumables = 3;
    public void AddPassive(SO_PassiveUpgrade upgrade)
    {
        if (!_passives.Contains(upgrade))
        {
            _passives.Add(upgrade);
            Debug.Log($"[Inventory] {upgrade.name} aggiunto allo zaino");
        }
    }

    public void AddConsumable(SO_ConsumableUpgrade upgrade)
    {
        
        if (_consumables.Count < _maxConsumables)
        {
            _consumables.Add(upgrade);
            Debug.Log($"[Inventory] {upgrade.name} aggiunto. Spazio : {_consumables.Count}/{_maxConsumables}");
        }
        else
        {
            Debug.Log($"[Inventory] Inventario pieno");
        }
    }

    public bool CanAdd()
    {
        return _consumables.Count < _maxConsumables;
    }

    public bool HaveConsumable(SO_ConsumableUpgrade consumableUpgrade)
    {
        return _consumables.Contains(consumableUpgrade);
    }
    public void UseUpgrade(SO_ConsumableUpgrade upgrade)
    {
        if (_consumables.Contains(upgrade))
        {
            Debug.Log($"{upgrade.name} Estato utilizzato. Spazio : {_maxConsumables - _consumables.Count}");
            _consumables.Remove(upgrade);
            upgrade.IsUnlocked = false;
        }
        
    }
}
