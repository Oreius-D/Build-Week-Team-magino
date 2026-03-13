using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolSettings : ScriptableObject
{
    public bool CollectionCheck=false;
    public int DefaualtCapacity = 10;
    public int MaxSize = 50;
    public float LifeTime = 3f;
    public PoolId Id;
    public GameObject Prefab;
    public bool Preload = false;

    public virtual PooledObject Create()
    {
        PooledObject go = Instantiate(Prefab).GetComponent<PooledObject>();
        go.gameObject.SetActive(false);
        go.transform.SetParent(PoolManager.Instance.transform);
        return go;
    }
    public virtual void Get(PooledObject go)
    {
        go.gameObject.SetActive(true);
    }
    public virtual void Release(PooledObject go)
    {
        go.gameObject.SetActive(false) ;
    }
    public virtual void Destroy(PooledObject go)
    {
        Destroy(go.gameObject);
    }
}
