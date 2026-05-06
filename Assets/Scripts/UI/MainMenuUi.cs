using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlaySound(SoundID.Button);
        Invoke(nameof(DelayStartGame), 1);
    }
    public void DelayStartGame()
    {
        SceneManager.LoadScene("Alessandro2");
    }
}
