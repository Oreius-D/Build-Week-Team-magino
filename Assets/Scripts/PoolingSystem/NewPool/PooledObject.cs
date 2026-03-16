using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] PoolSettings poolFrom;
    [SerializeField] float despawnDistance = 30f;

    void Update()
    {
        if (transform.position.z < Pool.Instance.Player.position.z - despawnDistance)
        {
            Pool.Instance.ReturnToPool(PoolId.Coin, gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pool.Instance.ReturnToPool(poolFrom.Id,transform.parent.gameObject);
        }
    }
}
