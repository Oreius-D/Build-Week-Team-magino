using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runner/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    //Configurazione del movimento del player
    [Header("Movement")]
    public float forwardSpeed = 5f; // Velocità di movimento in avanti
    public float laneChangeSpeed = 10f; // Velocità di cambio corsia
    public float laneOffset = 2f; // Distanza tra le corsie

    //Configurazione del salto
    [Header("Jump")]
    public float jumpForce = 10f; // Forza del salto
    public float gravity = -9.81f; // Gravità applicata al player
    public float groundedStickingForce = -5f; // Forza che tiene il player attaccato al suolo

    //Configurazione dello sliding del player
    [Header("Slide")]
    public float slideDuration = 1f; // Durata dello slide in secondi
    public float slideHeightMultiplier = 0.5f; // Moltiplicatore per l'altezza del player durante lo slide
    public float slideCenterYMultiplier = 1.2f; // Moltiplicatore per la posizione del centro del player durante lo slide

    //Gestione invulnerabilità dopo un colpo subito
    [Header("Invulnerability")]
    public float invulnerabilityDuration = 2f; // Durata dell'invulnerabilità in secondi
}
