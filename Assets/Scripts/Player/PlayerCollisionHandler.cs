using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [Header("Layers (names in Unity)")]
    [SerializeField] private string coinLayerName = "Coin";
    [SerializeField] private string obstacleLayerName = "Obstacle";

    [Header("Options")]
    [Tooltip("Evita doppi trigger nello stesso frame (utile se un oggetto ha più collider).")]
    [SerializeField] private bool preventDuplicateSameFrame = true;

    private int coinLayer;
    private int obstacleLayer;

    private PlayerHealth health;
    private IPlayerInventory inventory;
    private RunCurrency runCurrency;

    private int lastTriggeredId;
    private int lastTriggeredFrame;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        inventory = GetComponent<InventoryLink>()?.Inventory;
        runCurrency = GetComponent<RunCurrency>();

        coinLayer = LayerMask.NameToLayer(coinLayerName);
        obstacleLayer = LayerMask.NameToLayer(obstacleLayerName);

        if (coinLayer < 0)
            Debug.LogError($"Layer '{coinLayerName}' non esiste. Crealo in Tags and Layers.", this);
        if (obstacleLayer < 0)
            Debug.LogError($"Layer '{obstacleLayerName}' non esiste. Crealo in Tags and Layers.", this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health == null || health.IsDead) return;

        int layer = other.gameObject.layer;

        // --- COIN ---
        if (layer == coinLayer)
        {
            if (preventDuplicateSameFrame)
            {
                int coinId = other.transform.root.gameObject.GetInstanceID();
                if (lastTriggeredFrame == Time.frameCount && lastTriggeredId == coinId)
                    return;

                lastTriggeredFrame = Time.frameCount;
                lastTriggeredId = coinId;
            }

            runCurrency?.AddCoins(1);
            other.gameObject.SetActive(false);
            return;
        }

        // --- OBSTACLE ---
        if (layer == obstacleLayer)
        {
            // Calcola un ID stabile per "questo ostacolo", non per il singolo collider
            var obstacleRef = other.GetComponentInParent<ObstacleConfigRef>();
            int obstacleId = obstacleRef != null
                ? obstacleRef.gameObject.GetInstanceID()
                : other.transform.root.gameObject.GetInstanceID();

            if (preventDuplicateSameFrame)
            {
                if (lastTriggeredFrame == Time.frameCount && lastTriggeredId == obstacleId)
                    return;

                lastTriggeredFrame = Time.frameCount;
                lastTriggeredId = obstacleId;
            }

            // scudo
            if (inventory != null && inventory.TryAbsorbHit())
                return;

            int damage = 1;
            if (obstacleRef != null)
                damage = obstacleRef.Damage;

            // Debug utile
            Debug.Log($"Hit by obstacle '{other.name}' dmg={damage} invuln={config.invulnerabilityDuration}");

            health.TryTakeHit(damage, config.invulnerabilityDuration);
            return;
        }
    }
}