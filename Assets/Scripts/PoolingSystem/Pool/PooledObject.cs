using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private PoolSettings poolFrom;
    [SerializeField] float despawnDistance = 10f;

    public PoolSettings PoolFrom { get => poolFrom; }

    void Update()
    {
        if (transform.position.z < Pool.Instance.Player.position.z - despawnDistance)
        {
            ResetPoolObject();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetPoolObject();
        }
    }
    private void ResetPoolObject()
    {
        Pool.Instance.ReturnToPool(poolFrom.Id,gameObject);
    }
}
