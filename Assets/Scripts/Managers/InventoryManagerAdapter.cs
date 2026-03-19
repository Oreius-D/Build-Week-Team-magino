using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerAdapter : MonoBehaviour ,IPlayerInventory
{
    private InventoryManager manager;// riferimento a inventorymanager

    public int RunCoins => manager.coins;//metto le monete del manager con interfaccia
    [SerializeField] private RunCurrency runCurrency;

    
    private void Start()
    {
        manager = InventoryManager.Instance;//collego l'istanza
        Debug.Log(manager);
        runCurrency.OnCoinsChanged += AddRunCoins;
    }

    private void OnDisable()
    {
        runCurrency.OnCoinsChanged -= AddRunCoins;
    }
    public bool TryUseConsumable(int slotIndex)
    {
        //se non ce non faccio nulla
        if (manager.saveData.ConsumableID == null) return false;
        //Controllo se e maggiore di 0 e minore del num di oggetti che ho
        if (slotIndex >= 0 && slotIndex < manager.saveData.ConsumableID.Count)
        {
            SO_ConsumableUpgrade itemUsable = manager.saveData.ConsumableID[slotIndex];
            if (itemUsable != null)
            {
                //uso oggetto
                manager.UseUpgrade(itemUsable);
                Debug.Log($"Usato oggetto {slotIndex} : {itemUsable.NameUpgrade}");
                return true;
            }
        }
        Debug.Log($"Slot vuoto {slotIndex}");
        return false;
    }
    public void AddRunCoins(int amount)
    {
        manager.coins += amount;//modifico i coin nel manager
        Debug.Log($"monete aggiunte {amount}. Totale : {manager.coins}");
    }

    public bool HasPassive (PassiveType passive)
    {
        //cerco nella lista delle passive del inventario se esiste la passiva richiesta
        foreach (SO_PassiveUpgrade p in manager.saveData.PassiveID)
        {
            if (p.type == passive)
            {
                return true;
            }
            
        }
        return false;
    }
   
    public bool TryAbsorbHit()
    {
        SO_PassiveUpgrade shield = null;
        //cerco se ho lo scudo
        foreach (var p in manager.saveData.PassiveID)
        {
            if (p.type == PassiveType.Shield)
            {
                shield = p;
                break;
            }
        }
        if (shield != null)
        {
            //lo trovo e lo uso
            manager.saveData.PassiveID.Remove(shield);
            Debug.Log("Colpo assorbito");
            return true;
        }
        return false;
    }
    
    
}
