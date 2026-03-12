using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [Header("Layers")]
    [SerializeField] private int coinLayer = 6;
    [SerializeField] private int obstacleLayer = 7;

    private PlayerHealth health;
    private IPlayerInventory inventory;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        inventory = GetComponent<InventoryLink>()?.Inventory;

        coinLayer = LayerMask.NameToLayer("Coin");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (health.IsDead) return;

        int layer = other.gameObject.layer;

        if (layer == coinLayer)
        {
            inventory?.AddRunCoins(1);
            other.gameObject.SetActive(false);
            return;
        }

        if (layer == obstacleLayer)
        {
            if (inventory != null && inventory.TryAbsorbHit())
                return;

            health.TryTakeHit(config.invulnerabilityDuration);
        }
    }
}
