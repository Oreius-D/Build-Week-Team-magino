using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Transform[] coinPoints;
    [SerializeField] private PoolSettings coinPool;
    [SerializeField] private int numCoins;
    [SerializeField] private float offsetCoin = 2f;
    public void SpawnCoin()
    {
        Vector3 offset = Vector3.forward * offsetCoin;
        foreach (var coinPoint in coinPoints)
        {
            for (int i = 0; i < numCoins; i++)
            {
                var obj = Pool.Instance.GetItemPool(coinPool);
                obj.transform.position = coinPoint.position + offset * i;

            }
        }
        
    }

}
