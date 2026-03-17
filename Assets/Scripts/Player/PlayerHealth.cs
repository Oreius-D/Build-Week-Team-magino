using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> OnHit;   // Hit rimanenti, utile per aggiornare UI o triggerare effetti
    public event Action OnDied;       // Il giocatore è morto, utile per gestire game over o respawn

    [SerializeField] private int maxHits = 3; // Numero di hit che il giocatore può subire prima di morire

    private int hitsLeft;       // Hit rimanenti
    private float invulnUntil;  // Timestamp fino a cui il giocatore è invulnerabile dopo un hit

    public int HitsLeft => hitsLeft;            // Esposizione pubblica degli hit rimanenti
    public bool IsDead { get; private set; }    // Stato di morte del giocatore
    public bool IsInvulnerable => Time.time < invulnUntil;
    // Il giocatore è invulnerabile se il tempo attuale è inferiore al timestamp di invulnerabilità

    private PlayerAnimator playerAnimator;
    private void Awake()
    {
        playerAnimator=GetComponentInChildren<PlayerAnimator>();
    }
    // Inizializza la salute del giocatore all'inizio del gioco
    public void ResetHealth()
    {
        IsDead = false;
        hitsLeft = maxHits;

        // Nota: se StartTime = 0, significa "non invulnerabile" subito
        invulnUntil = (float)Costants.StartTime;

        Debug.Log($"Player health reset: hitsLeft={hitsLeft}, invulnerableUntil={invulnUntil}");
    }

    // Tenta di far subire un hit al giocatore. 
    // Restituisce true se l'hit è stato subito, false se il giocatore è morto o invulnerabile.
    public bool TryTakeHit(int damage, float invulnDuration)
    {
        if (IsDead) return false;          // Se il giocatore è già morto, non subisce ulteriori hit
        if (IsInvulnerable) return false;  // Se il giocatore è invulnerabile, non subisce l'hit

        // Clamp difensivo: niente danno negativo o zero
        damage = Mathf.Max(1, damage);

        // Riduce gli hit rimanenti, assicurandosi di non scendere sotto 0 (Dead)
        hitsLeft = Mathf.Max((int)Costants.Dead, hitsLeft - damage);

        // Notifica gli ascoltatori del nuovo numero di hit rimanenti
        OnHit?.Invoke(hitsLeft);
        playerAnimator.Hit();

        // Imposta il tempo di invulnerabilità dopo aver subito un hit
        invulnUntil = Time.time + Mathf.Max(0f, invulnDuration);

        Debug.Log($"Player took hit: damage={damage}, hitsLeft={hitsLeft}, invulnerableUntil={invulnUntil}");

        // Se gli hit rimanenti sono 0, il giocatore è morto
        // Importante: usare <= evita casi limite (es. danno > hits rimasti, o costanti non int).
        if (hitsLeft <= (int)Costants.Dead)
        {
            IsDead = true;
            playerAnimator.Death();
            Debug.Log("Player died");
            OnDied?.Invoke();
        }

        return true;
    }
}
