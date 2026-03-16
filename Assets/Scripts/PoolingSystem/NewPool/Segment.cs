using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] Transform lanePoints;
    [SerializeField] private int numCoins;
    [SerializeField] private float offsetCoin=2f;
    public void SpawnCoin()
    {
        Debug.Log(lanePoints.position);
        Vector3 offset = Vector3.forward*offsetCoin;
        for (int i = 0; i < numCoins; i++)
        {
            var obj = Pool.Instance.GetItemPool(PoolId.Coin);
            obj.transform.position = lanePoints.position+offset*i;
         
        }
    }

}
