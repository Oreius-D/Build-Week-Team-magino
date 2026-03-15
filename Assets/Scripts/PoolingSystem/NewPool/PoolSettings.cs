using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pool")]
public class PoolSettings : ScriptableObject
{
    public PoolId Id;
    public int DefaultSize;
    public int MaxSize;
    public GameObject prefab;
}
