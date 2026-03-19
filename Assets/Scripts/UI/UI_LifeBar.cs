using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class UI_LifeBar : MonoBehaviour
{
    [SerializeField] private Image[] hearts;

    private Sprite fullHeart;
    private Sprite emptyHeart;

    
    public void UpdateHeartsGraphics()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //if (i<)
        }
    }

    //Action<int> OnHit
    //PlayerHealth.OnHit += mia funzione senza parentesi
}
