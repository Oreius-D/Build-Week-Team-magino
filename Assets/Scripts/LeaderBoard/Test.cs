using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public LeaderBoardManager leaderboardManager;

    void Update()
    {
        // Se premo Spazio, aggiungo un punteggio e salvo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int points = Random.Range(100, 1000);
            string name = "Player_" + Random.Range(1, 100);

            Debug.Log($"Aggiungo: {name} con {points} punti");
            leaderboardManager.AddScore(name, points);
            leaderboardManager.Save();
            Debug.Log("Punteggio salvato su disco!");
        }

        // Se premo L, carico e stampa la classifica attuale
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("--- CLASSIFICA ATTUALE ---");
            
            leaderboardManager.Load();
        }
    }
}
