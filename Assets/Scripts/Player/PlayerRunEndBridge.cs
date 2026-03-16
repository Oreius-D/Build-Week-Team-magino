using UnityEngine;

public class PlayerRunEndBridge : MonoBehaviour
{
    private PlayerHealth health;
    private RunCurrency runCurrency;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        runCurrency = GetComponent<RunCurrency>();
    }

    private void OnEnable()
    {
        if (health != null) health.OnDied += HandleDied;
    }

    private void OnDisable()
    {
        if (health != null) health.OnDied -= HandleDied;
    }

    private void HandleDied()
    {
        // restituisce monete run a chi gestisce save/game over
        runCurrency?.EndRun();
    }
}
