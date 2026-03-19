using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] SO_CoinData coinData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //do soldi al player
            //Debug.Log($"{coinData.Value} e suono {coinData.SoundId}");
           // AudioManager.Instance.PlaySound(coinData.SoundId);
        }
    }
}
