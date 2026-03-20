using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string path;
    [SerializeField] private int maxPlayerInLeaderboard = 5;
    //private List<SO_PassiveUpgrade> passiveList;
    //private List<SO_ConsumableUpgrade> consumableList;
    //private SaveData currentSave = new SaveData();
    private void Start()
    {
        path = Application.persistentDataPath + "/SaveGame.json";
    }
    public void SaveGame()
    {
        SaveData currentSave = InventoryManager.Instance.saveData;
        //Inventoiry.Inastance.SaveData
        //currentSave.PassiveID.Clear();
        //currentSave.ConsumableID.Clear();
        //currentSave.Banana = InventoryManager.Instance.saveData.Banana;
        ////Ciclo le passive in gioco
        //foreach (var passive in passiveList)
        //{
        //    if (passive.IsUnlocked)//Se la passiva è TRUE
        //    {
        //        currentSave.PassiveID.Add(passive.ID);//Aggiungo ID
        //    }
        //}
        ////Ciclo i consumabili ingioco
        //foreach (var consumable in consumableList)
        //{
        //    currentSave.ConsumableID.Add(consumable.ID);//Aggiungo ID
        //}
        //Converto sataSaving in json
        Debug.Log(currentSave);
        string json = JsonUtility.ToJson(currentSave , true);//Da FARE = dataSaving dell' invenatrio
        //Scrivo il file nella memoria
        File.WriteAllText(Application.persistentDataPath + "/SaveGame.json",json);
        Debug.Log($"Gioco salvato");
        
    }
    public void Load()
    {
        
        //Se non esiste non facciamo nulla(Tipo se e la prima volta che si gioca)
        if (!File.Exists(path)) return;
        //Leggo il testo nel Path
        string json = File.ReadAllText(path);
        //Converto il testo in un oggetto
        SaveData saveLoad = JsonUtility.FromJson<SaveData>(json);
        InventoryManager.Instance.saveData = saveLoad;
        //Qua ciclo tutti le passive che abbiamo nel salvataggio
        //foreach (string passive in currentSave.PassiveID)
        //{
        //    //per ogni passiva,cerco nella lista di tutte le passive del negozio quella uguale
        //    foreach (SO_PassiveUpgrade passiveUpgrade in passiveList )
        //    {
        //        if (passiveUpgrade.ID == passive)
        //        {
        //            //se trovata  la sblocco e la metto nell'inventario
        //            passiveUpgrade.IsUnlocked = true;
        //            InventoryManager.Instance.AddPassive(passiveUpgrade);
        //            break;//Esco perche l'ho trovata
        //        }
        //    }
        //}
        ////ciclo per i consumabili
        //foreach (string consumable in currentSave.ConsumableID)
        //{
        //    //Se l'inventario è pieno non aggiungo nulla
        //    if (!InventoryManager.Instance.CanAdd())
        //    {
        //        break;
        //    }
        //    //Cerco oggetto corrispondete a "consumable"
        //    foreach (SO_ConsumableUpgrade consumableUpgrade in  consumableList )
        //    {
        //        if (consumableUpgrade.ID == consumable)
        //        {
        //            //Se trovato lo aggiungo
        //            InventoryManager.Instance.AddConsumable(consumableUpgrade);
        //            break;
        //        }
        //    }
        //}

    }
    public void AddScore()
    {
        SaveData saveLeader = InventoryManager.Instance.saveData;
        int currentScore = Mathf.FloorToInt(Pool.Instance.Player.position.z);
        string playerName = "Player" + (saveLeader.LeaderBoard.Count + 1);
        //aggiungo il punteggio e nome personaggio
        saveLeader.LeaderBoard.Add(new Scores { PlayerName = playerName, Point = currentScore });
        //ordino la lista , confronto la y con la x e diventa crescente
        saveLeader.LeaderBoard.Sort((x, y) => y.Point.CompareTo(x.Point));
        // metto un massimo di player in lista, tipo TOP. es i migliori 5 vengono registrari
        if (saveLeader.LeaderBoard.Count > maxPlayerInLeaderboard)
        {
            saveLeader.LeaderBoard.RemoveAt(maxPlayerInLeaderboard);
        }
        SaveGame();
    }
}
