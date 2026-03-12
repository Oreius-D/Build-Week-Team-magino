using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> OnHit;   // Hit rimanenti, utile per aggiornare UI o triggerare effetti
    public event Action OnDied; // Il giocatore è morto, utile per gestire game over o respawn

    [SerializeField] private int maxHits = 3; // Numero di hit che il giocatore può subire prima di morire

    private int hitsLeft; // Hit rimanenti
    private float invulnUntil; // Timestamp fino a cui il giocatore è invulnerabile dopo un hit

    public int HitsLeft => hitsLeft; // Esposizione pubblica degli hit rimanenti
    public bool IsDead { get; private set; } // Stato di morte del giocatore
    public bool IsInvulnerable => Time.time < invulnUntil; // Il giocatore è invulnerabile se il tempo attuale è inferiore al timestamp di invulnerabilità

    // Inizializza la salute del giocatore all'inizio del gioco
    public void ResetHealth()
    {
        IsDead = false;
        hitsLeft = maxHits;
        invulnUntil = ((float)Costants.StartTime);
    }

    // Tenta di far subire un hit al giocatore. Restituisce true se l'hit è stato subito, false se il giocatore è morto o invulnerabile.
    public bool TryTakeHit(float invulnDuration)
    {
        if (IsDead) return false; // Se il giocatore è già morto, non subisce ulteriori hit
        if (IsInvulnerable) return false; // Se il giocatore è invulnerabile, non subisce l'hit

        // Subisce l'hit, decrementa gli hit rimanenti e aggiorna lo stato di invulnerabilità
        hitsLeft--;
        OnHit?.Invoke(hitsLeft); // Notifica gli ascoltatori del nuovo numero di hit rimanenti

        // Imposta il tempo di invulnerabilità dopo aver subito un hit
        invulnUntil = Time.time + invulnDuration;

        // Se gli hit rimanenti sono 0, il giocatore è morto
        if (Costants.Dead.Equals(hitsLeft))
        {
            IsDead = true;
            OnDied?.Invoke();
        }
        return true;
    }
}
