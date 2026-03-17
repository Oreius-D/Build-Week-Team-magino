using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController controller; // Riferimento al PlayerController, da assegnare in inspector o trovato in Awake
    private PlayerAnimator playerAnimator;

    [Header("Keyboard Bindings")]
    [SerializeField] private KeyCode moveLeft = KeyCode.A;
    [SerializeField] private KeyCode moveRight = KeyCode.D;

    [SerializeField] private KeyCode jump = KeyCode.Space;     // salto (e doppio salto: stesso tasto)
    [SerializeField] private KeyCode slide = KeyCode.S;

    [SerializeField] private KeyCode consumable1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode consumable2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode consumable3 = KeyCode.Alpha3;

    private IPlayerInventory inventory;

    private void Awake()
    {
        if (controller == null)
            controller = GetComponent<PlayerController>();

        playerAnimator = GetComponentInChildren<PlayerAnimator>();

        // Inventario esterno tramite InventoryLink (o adapter)
        inventory = controller != null ? controller.GetComponent<InventoryLink>()?.Inventory : null;
        if (inventory == null)
            inventory = GetComponent<InventoryLink>()?.Inventory; // fallback se PlayerInput è sullo stesso GO del link
    }
    private void Start()
    {
        playerAnimator.Run();
    }
    private void Update()
    {
        if (controller == null) return;

        // --- Move lanes ---
        // Supporto anche frecce, indipendentemente dai bind
        if (Input.GetKeyDown(moveLeft) || Input.GetKeyDown(KeyCode.LeftArrow))
            controller.MoveLane(-1);

        if (Input.GetKeyDown(moveRight) || Input.GetKeyDown(KeyCode.RightArrow))
            controller.MoveLane(+1);

        // --- Jump (includes Double Jump) ---
        // Il doppio salto non è un input separato: è il Controller che lo concede se l'inventario ha il passivo.
        if (Input.GetKeyDown(jump) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            controller.Jump();
            playerAnimator.Jump();
        }

        // --- Slide ---
        if (Input.GetKeyDown(slide) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            controller.Slide();
            playerAnimator.Slide();
        }

        // --- Consumables 1-3 ---
        if (Input.GetKeyDown(consumable1)) inventory?.TryUseConsumable(0);
        if (Input.GetKeyDown(consumable2)) inventory?.TryUseConsumable(1);
        if (Input.GetKeyDown(consumable3)) inventory?.TryUseConsumable(2);

        // --- Fast Fall (caduta rapida) ---
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (controller.IsGrounded) controller.Slide();
            else controller.FastFall(true);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            controller.FastFall(false);
        }
    }
}
