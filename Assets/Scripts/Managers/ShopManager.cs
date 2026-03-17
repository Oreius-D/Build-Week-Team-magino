using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShopManager : MonoBehaviour
{
    //PRENDO LE MONETE DALL INVENTARIO NON PIU DALL GAMEDATA


    //Lista dei Scriptable Objects
    [SerializeField] private List<SO_ConsumableUpgrade> shopConsumable;
    [SerializeField] private List<SO_PassiveUpgrade> shopPassice;
    //Bottone dell'acquisto
    [SerializeField] private GameObject buyButton;

    [SerializeField] private SaveManager saveManager;



    //Funzione per acquistare
    public void BuyUpgrade(SO_UpgradeItems items)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.saveData.Banana < items.Cost)
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
            
            InventoryManager.Instance.saveData.Banana -= passive.Cost;
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
            InventoryManager.Instance.saveData.Banana -= consumable.Cost;
            InventoryManager.Instance.AddConsumable(consumable);
            Debug.Log($"Consumabile aggiunto");
        }
        
        if (saveManager != null)
                { 
            saveManager.SaveGame();
            Debug.Log("Non e nullo");
        }//DEVO METTERCI IL SAVEDATA
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            saveManager.Load();
            Debug.Log("Loading");
        }
    }
}
