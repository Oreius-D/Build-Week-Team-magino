using UnityEngine;

public class InventoryLink : MonoBehaviour
{
    [Tooltip("Trascina qui l'inventario reale o un adapter che implementa IPlayerInventory")]
    [SerializeField] private MonoBehaviour inventoryBehaviour;

    public IPlayerInventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = inventoryBehaviour as IPlayerInventory;
        if (Inventory == null)
            Debug.LogError("InventoryLink: il riferimento non implementa IPlayerInventory", this);
    }
}