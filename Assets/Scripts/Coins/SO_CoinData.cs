using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Data")]
public class SO_CoinData : ScriptableObject
{
    [SerializeField] private int value;
    [SerializeField] private SoundID soundId;

    public int Value { get => value; }
    public SoundID SoundId { get => soundId; }
}
