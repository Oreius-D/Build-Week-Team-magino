using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [SerializeField] SO_CoinData coinData;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        audioSource.playOnAwake=false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //do soldi al player
            Debug.Log($"{coinData.Value} e suono {coinData.SoundId}");
        }
    }
}
