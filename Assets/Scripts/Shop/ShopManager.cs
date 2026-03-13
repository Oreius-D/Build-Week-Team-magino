using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShopManager : MonoBehaviour
{
    //Lista dei Scriptable Objects
    [SerializeField] private List<SO_ConsumableUpgrade> shopConsumable;
    [SerializeField] private List<SO_PassiveUpgrade> shopPassice;
    //Bottone dell'acquisto
    [SerializeField] private GameObject buyButton;
    //Dove ho salvato le monete, per il momento.Poi se è da cambiare vediamo
    [SerializeField] private GameData data;



    //Funzione per acquistare
    public void BuyUpgrade(SO_UpgradeItems items)
    {
        if (data != null && data.coins < items.Cost)
        {
            Debug.Log($"Soldi insufficenti");
            return;
        }
        if (items is SO_PassiveUpgrade passive)
        {
            if (passive.IsUnlocked)
            {
                Debug.Log($"Già acquistato");
                return;
            }
            
            data.coins -= passive.Cost;
            passive.IsUnlocked = true;
            InventoryManager.Instance.AddPassive(passive);
            buyButton.SetActive(false);
            Debug.Log("Upgrade passivo acquistato");
            
        }
        else if (items is SO_ConsumableUpgrade consumable)
        {
            if (!InventoryManager.Instance.CanAdd())
            {
                Debug.Log($"Inventario pieno");
                return;
            }
            data.coins -= consumable.Cost;
            InventoryManager.Instance.AddConsumable(consumable);
            Debug.Log($"Consumabile aggiunto");
        }
    }
}
