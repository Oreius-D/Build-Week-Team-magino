using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_CoinData : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private SoundID soundId;

    public int Value { get => value; }
    public SoundID SoundId { get => soundId; }
}
