using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [SerializeField] private RunCurrency playerCurrency;
    [SerializeField] private TextMeshProUGUI coinText;

    void Start()
    {
        if (playerCurrency == null) playerCurrency = FindObjectOfType<RunCurrency>();
        playerCurrency.OnCoinsChanged += UpdateCoinsGraphics;
        UpdateCoinsGraphics(playerCurrency.Coins);
    }

    private void UpdateCoinsGraphics(int currentCoins)
    {
        coinText.text = currentCoins.ToString();
    }

    void OnDestroy()
    {
        if (playerCurrency != null)
            playerCurrency.OnCoinsChanged -= UpdateCoinsGraphics;
    }
}
