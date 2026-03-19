using UnityEngine;
using UnityEngine.UI;

public class UI_LifeBar : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private PlayerHealth playerHealth;

    void Start()
    {
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.OnHit += OnPlayerHit;
        UpdateHeartsGraphics(playerHealth.HitsLeft);
    }

    public void OnPlayerHit(int hitsLeft)
    {
        UpdateHeartsGraphics(hitsLeft);
    }

    public void UpdateHeartsGraphics(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) return;

            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHit -= OnPlayerHit;
    }
}
