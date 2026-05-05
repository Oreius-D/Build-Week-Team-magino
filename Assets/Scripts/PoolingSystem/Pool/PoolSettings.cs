using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool")]
public class PoolSettings : ScriptableObject
{
    public PoolId Id;
    public int numPerPrefab;
    public GameObject[] Prefabs;
}
