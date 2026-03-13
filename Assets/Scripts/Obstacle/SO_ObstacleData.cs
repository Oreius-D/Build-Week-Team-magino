using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Obstacle Data")]
public class SO_ObstacleData : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private SoundID soundID;

    public int Damage { get => damage; }
    public SoundID SoundID { get => soundID; }
}
