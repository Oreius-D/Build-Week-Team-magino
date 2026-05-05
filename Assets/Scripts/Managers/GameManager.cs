using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _onGameOver;
    [SerializeField] private UnityEvent _onWin;

    [SerializeField] SoundID buttonSoundID;
    [SerializeField] SoundID musicID;

    public void GameOver()
    {
        Invoke(nameof(DelayGameOver), 1);
    }
    public void DelayGameOver()
    {
        _onGameOver?.Invoke();
        AudioManager.Instance.PlayMusic(musicID);
    }

    // gestione bottoni

    public void MainMenu()
    {
        Invoke(nameof(DelayMainMenu), 1);
    }

    public void DelayMainMenu()
    {
        AudioManager.Instance.PlaySound(buttonSoundID);
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
        Invoke(nameof(DelayPlayGame), 1);

    }

    public void DelayPlayGame()
    {
        AudioManager.Instance.PlaySound(buttonSoundID);
        SceneManager.LoadScene("Alessandro2");
    }
    public void Retry()
    {
        Invoke(nameof(DelayRetry), 1);
    }

    public void DelayRetry()
    {
        AudioManager.Instance.PlaySound(buttonSoundID);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
