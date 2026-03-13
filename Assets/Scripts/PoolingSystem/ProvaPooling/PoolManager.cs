using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    Dictionary<PoolId, IObjectPool<PooledObject>> pools = new();

    public void Preload(PoolSettings settings)
    {
        var pool = GetPool(settings);
    }

    private IObjectPool<PooledObject> GetPool(PoolSettings settings)
    {
        if (pools.TryGetValue(settings.Id, out var pool))
            return pool;
        pool= new ObjectPool<PooledObject>(
            settings.Create,
            settings.Get,
            settings.Release,
            settings.Destroy,
            settings.CollectionCheck,
            settings.DefaualtCapacity,
            settings.MaxSize       
            );
        pools.Add(settings.Id, pool);
        return pool;
    }
}
