using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;
    private void Start()
    {
        PlayerHealth.OnDied += StartGameOverUi;
    }
    private void OnDestroy()
    {
        PlayerHealth.OnDied -= StartGameOverUi;
    }
    private void StartGameOverUi()
    {
        Invoke(nameof(DelayGameOver), 4f);
    }
    private void DelayGameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void StartRun()
    {
        AudioManager.Instance.PlaySound(SoundID.Button);
        Invoke(nameof(DelayStart), 1);
    }
    private void DelayStart()
    { 
        SceneManager.LoadScene("Alessandro2");
    }
    public void GoToShop()
    {
        AudioManager.Instance.PlaySound(SoundID.Button);
        AudioManager.Instance.PlayMusic(SoundID.ShopMusic);
        Invoke(nameof(DelayGoToShop), 1);
    }
    private void DelayGoToShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySound(SoundID.Button);
        SceneManager.LoadScene("MainMenu");
    }
}
