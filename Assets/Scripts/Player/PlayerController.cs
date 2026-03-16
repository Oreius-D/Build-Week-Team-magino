using System;
using UnityEngine;

// Gestisce movimento, salto, slide e interazione con salute/inventario del giocatore
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Eventi per notificare cambi di corsia (utile per animazioni o effetti)
    public event Action<int> OnLaneChanged;

    // Configurazione del giocatore, da ScriptableObject per facilità di tuning
    [SerializeField] private PlayerConfig config;

    // Componenti
    private CharacterController cc;

    // Corsia
    private int currentLane = 1; // 0,1,2
    private float targetX;

    // Vercitcalità e salto
    private float verticalVel;
    //ToDo: Attivare con inventario esterno
    private bool hasDoubleJumped;

    // Cambio di altezza per slide
    private bool isSliding;
    private float slideEndTime;
    private float originalHeight;
    private Vector3 originalCenter;

    // Flag per il fast fall (caduta rapida): attivo quando il giocatore preme giù in aria, aumenta la gravità
    private bool fastFalling;

    // Dipendenze da altri componenti (salute, inventario)
    private PlayerHealth health;
    private IPlayerInventory inventory;

    public int CurrentLane => currentLane; // Esposizione pubblica della corsia attuale
    public bool IsGrounded => cc.isGrounded; // Esposizione pubblica dello stato di terra (utile per animazioni o effetti)

    private void Awake()
    {
        cc = GetComponent<CharacterController>(); // Assicurato da RequireComponent
        health = GetComponent<PlayerHealth>();

        //Prendere l'invetario
        inventory = GetComponent<InventoryLink>()?.Inventory;
        if (inventory == null)
            Debug.LogWarning("Inventory non trovato: double jump/shield/consumabili disattivi", this);

        health.ResetHealth();

        // Salva altezza e centro originali per poterli ripristinare dopo lo slide
        originalHeight = cc.height;
        originalCenter = cc.center;
    }

    // Resetta stato e posizione per un nuovo run (chiamato da GameManager)
    public void ResetForNewRun(Vector3 startPos, int startLane = 1)
    {
        transform.position = startPos; // Posizione di partenza, tipicamente davanti alla telecamera
        currentLane = Mathf.Clamp(startLane, 0, 2); // Corsia di partenza, default 1 (corsia centrale)
        targetX = LaneToX(currentLane); // Calcola posizione X target per la corsia

        verticalVel = 0f; // Reset velocità verticale
        hasDoubleJumped = false; // Reset stato double jump

        StopSlideImmediate(); // Assicura che non si parta in slide

        health.ResetHealth(); // Resetta salute

        OnLaneChanged?.Invoke(currentLane); // Notifica cambio corsia (utile se il run precedente è finito in una corsia diversa)
    }

    private float LaneToX(int lane) => (lane - 1) * config.laneOffset; // Converte indice corsia in posizione X (0 -> -offset, 1 -> 0, 2 -> +offset)

    private void Update()
    {
        if (health.IsDead) return; // Se il giocatore è morto, non processa input o movimento

        // Movimento in avanti costante
        Vector3 move = Vector3.forward * config.forwardSpeed;

        // Movimento laterale verso la posizione target della corsia, con smoothing
        float x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * config.laneChangeSpeed);
        float deltaX = x - transform.position.x;
        move += Vector3.right * (deltaX / Mathf.Max(Time.deltaTime, 0.0001f));

        // Ground check e gravità
        if (cc.isGrounded)
        {
            if (verticalVel < 0f) verticalVel = config.groundedStickingForce;
            hasDoubleJumped = false;
        }

        //Gestione gravità e fast fall (caduta rapida)
        float g = config.gravity;
        if (!cc.isGrounded && fastFalling && verticalVel > config.gravity) // sei in aria
            g *= config.fastFallMultiplier;

        verticalVel += g * Time.deltaTime;

        // clamp (opzionale)
        verticalVel = Mathf.Max(verticalVel, -config.maxFallSpeed);

        verticalVel += config.gravity * Time.deltaTime;
        move += Vector3.up * verticalVel;

        // Timing per terminare lo slide
        if (isSliding && Time.time >= slideEndTime)
            StopSlideImmediate();

        // Applica movimento al CharacterController
        cc.Move(move * Time.deltaTime);
    }

    // Comandi per input esterni (da InputManager o UI)
    public void MoveLane(int dir) // -1 = sinistra, +1 = destra
    {
        if (health.IsDead) return; // Se il giocatore è morto, non processa input

        int newLane = Mathf.Clamp(currentLane + dir, 0, 2); // Calcola nuova corsia, limitata tra 0 e 2

        if (newLane == currentLane) return; // Se la corsia non cambia, non fare nulla

        currentLane = newLane; // Aggiorna corsia attuale
        targetX = LaneToX(currentLane); // Aggiorna posizione X target per la nuova corsia
        OnLaneChanged?.Invoke(currentLane); // Notifica cambio corsia (utile per animazioni o effetti)
    }

    public void Jump()
    {
        if (health.IsDead) return; // Se il giocatore è morto, non processa input

        if (isSliding) StopSlideImmediate(); // Se stiamo slideando, interrompi lo slide per permettere il salto

        // Salto solo a terra
        if (cc.isGrounded)
        {
            verticalVel = Mathf.Sqrt(config.jumpForce * -2f * config.gravity);
            return;
        }

        // Secondo salto: solo se passivo attivo e non ancora usato
        bool canDoubleJump = inventory != null && inventory.HasPassive(PassiveType.DoubleJump);
        if (canDoubleJump && !hasDoubleJumped)
        {
            hasDoubleJumped = true;

            verticalVel = Mathf.Sqrt(config.jumpForce * -2f * config.gravity);
        }

    }

    // Slide solo a terra, con durata limitata. Modifica altezza e centro del CharacterController per l'effetto visivo e di collisione.
    public void Slide()
    {
        if (health.IsDead) return; // Se il giocatore è morto, non processa input
        if (isSliding) return; // Se stiamo già slideando, non fare nulla

        // Slide solo a terra
        if (!cc.isGrounded) return;

        isSliding = true; // Inizia lo slide
        slideEndTime = Time.time + config.slideDuration; // Imposta il tempo di fine dello slide

        // Modifica altezza e centro del CharacterController per l'effetto slide
        cc.height = originalHeight * config.slideHeightMultiplier;
        cc.center = new Vector3(originalCenter.x, originalCenter.y * config.slideCenterYMultiplier, originalCenter.z);
    }

    // Interrompe immediatamente lo slide, ripristinando altezza e centro originali. Utile per quando si salta durante lo slide o si muore.
    private void StopSlideImmediate()
    {
        isSliding = false;
        cc.height = originalHeight;
        cc.center = originalCenter;
    }

    //Metodo che attiva il fast fall (caduta rapida) quando il giocatore preme giù in aria, aumentando la gravità
    public void FastFall(bool active)
    {
        fastFalling = active;
    }
}
