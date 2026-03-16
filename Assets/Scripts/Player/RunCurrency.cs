using System;
using UnityEngine;

public class RunCurrency : MonoBehaviour
{
    public event Action<int> OnCoinsChanged;   // aggiorna UI
    public event Action<int> OnRunEnded;       // restituisce totale al GameManager/Save

    [SerializeField] private int coins;

    public int Coins => coins;

    public void ResetRun()
    {
        coins = 0;
        OnCoinsChanged?.Invoke(coins);
    }

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        coins += amount;
        Debug.Log($"Coins: {coins}");
        OnCoinsChanged?.Invoke(coins);
    }

    // Chiama questo quando la run finisce
    public void EndRun()
    {
        OnRunEnded?.Invoke(coins);
    }
}
