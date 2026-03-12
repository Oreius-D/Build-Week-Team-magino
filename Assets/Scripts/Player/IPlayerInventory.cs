using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInventory
{
    // Check oggetti passivi (double jump, shield)
    bool HasPassive(PassiveType passive);

    // Se lo scudo è attivo, assorbe un colpo e si disattiva
    bool TryAbsorbHit();

    // Metodi per gestire i run coins (monete raccolte durante la run)
    int RunCoins { get; }
    void AddRunCoins(int amount);

    // Metodi per gestire i consumabili
    bool TryUseConsumable(int slotIndex);
}
