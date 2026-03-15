using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] PoolSettings poolFrom;
    private float timer;
    private float interval=5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pool.Instance.ReturnToPool(poolFrom.Id,transform.parent.gameObject);
        }
    }
}
