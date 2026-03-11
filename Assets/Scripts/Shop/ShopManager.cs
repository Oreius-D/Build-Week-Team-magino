using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShopManager : MonoBehaviour
{
    //Lista dei Scriptable Objects
    [SerializeField] private List<SO_ConsumableUpgrade> _shopConsumable;
    [SerializeField] private List<SO_PassiveUpgrade> _shopPassice;
    //Bottone dell'acquisto
    [SerializeField] private GameObject _buyButton;
    //Dove ho salvato le monete, per il momento.Poi se è da cambiare vediamo
    [SerializeField] private GameData _data;



    //Funzione per acquistare
    public void BuyUpgrade(SO_UpgradeItems items)
    {
        if (_data != null && _data._coins < items.Cost)
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
            
            _data._coins -= passive.Cost;
            passive.IsUnlocked = true;
            InventoryManager.Instance.AddPassive(passive);
            _buyButton.SetActive(false);
            Debug.Log("Upgrade passivo acquistato");
            
        }
        else if (items is SO_ConsumableUpgrade consumable)
        {
            if (!InventoryManager.Instance.CanAdd())
            {
                Debug.Log($"Inventario pieno");
                return;
            }
            _data._coins -= consumable.Cost;
            InventoryManager.Instance.AddConsumable(consumable);
            Debug.Log($"Consumabile aggiunto");
        }
    }
}
