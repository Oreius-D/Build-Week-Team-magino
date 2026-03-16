using UnityEngine;

public class FakeInventory : MonoBehaviour, IPlayerInventory
{
    [Header("Toggle for testing")]
    public bool doubleJump = true;
    public bool shield = true;

    [Header("Shield test")]
    public bool shieldReady = true; // assorbe 1 hit e poi si scarica

    public int RunCoins { get; private set; }

    public bool HasPassive(PassiveType passive)
    {
        return passive switch
        {
            PassiveType.DoubleJump => doubleJump,
            PassiveType.Shield => shield,
            _ => false
        };
    }

    public bool TryAbsorbHit()
    {
        if (!shield) return false;
        if (!shieldReady) return false;

        shieldReady = false; // per test: assorbe una volta sola
        Debug.Log("Shield absorbed hit");
        return true;
    }

    public void AddRunCoins(int amount)
    {
        RunCoins += amount;
        Debug.Log($"RunCoins: {RunCoins}");
    }

    public bool TryUseConsumable(int slotIndex)
    {
        Debug.Log($"Used consumable slot {slotIndex}");
        return true;
    }
}
