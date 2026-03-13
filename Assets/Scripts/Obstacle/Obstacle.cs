using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private SO_ObstacleData obstacleData;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Eventuale TakeDamage Player
            //AudioManager.Instance.PlaySound(audioSource,obstacleData.SoundID);
            Debug.Log("ho colpito player");
            Debug.Log($"gli ho fatto {obstacleData.Damage} e ho suonato {obstacleData.SoundID}");
        }
    }
}
