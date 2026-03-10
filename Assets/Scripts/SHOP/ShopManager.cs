using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Lista dei Scriptable Objects
    [SerializeField] private List<SO_UpgradeItems> _shopUpgrade;
    //Bottone dell'acquisto
    [SerializeField] private GameObject _buyButton;
    //Dove ho salvato le monete, per il momento.Poi se è da cambiare vediamo
    [SerializeField] private GameData _data;

   

    //Funzione per acquistare
    public void BuyUpgrade(SO_UpgradeItems items)
    {
        //Se ho abbastanza soldi e non ho ancora il potenziamento lo acquisto
        if (_data != null && _data._coins >= items._cost && !items._isUnlocked)
        {
            //Faccio la sottrazione tra i soldi che ho e qaunto costa
            _data._coins -= items._cost;
            //Sblocco il potenziamento
            items._isUnlocked = true;
            Debug.Log($"Acquisto riuscito");
            //Disattivo il bottone
            _buyButton.SetActive(false);
        }
        else
        {
            Debug.Log($"Acquisto non riuscito");
        }
    }
}
