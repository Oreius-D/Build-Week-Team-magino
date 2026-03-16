using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;


public class LeaderBoardManager : MonoBehaviour
{
    private ListBoard scores = new ListBoard();
    string path;
    [SerializeField] private int maxPlayer;

    private void Start()
    {
        path = Application.persistentDataPath + "/Leaderboard.json";
        Load();
    }

    public void Save()
    {
        //prendo "scores" e lo trasformo in stringa, il true serve per leggerlo meglio nei file
        string json = JsonUtility.ToJson(scores,true);
        //scrive qeullo che ho appena creato (e sovrascrive"writhealltext") nel percoso path
        File.WriteAllText(path, json);
        
    }
    public void Load()
    {
        //  se il file esiste
        if (File.Exists(path))
        {
            //trasformo tutto in una stringa 
            string json = File.ReadAllText(path);
            //prendo la stringa e la ritrasformo
            scores = JsonUtility.FromJson<ListBoard>(json);
        }
    }
    public void AddScore(string name , int score)
    {
        //aggiungo il punteggio e nome personaggio
        scores.list.Add(new Scores { PlayerName = name, Point = score });
        //ordino la lista , confronto la y con la x e diventa crescente
        scores.list.Sort((x,y) => y.Point.CompareTo(x.Point));
        // metto un massimo di player in lista, tipo TOP. es i migliori 5 vengono registrari
        if (scores.list.Count > maxPlayer) 
        {
            scores.list.RemoveAt(maxPlayer); 
        }
        Save();
    }
}
