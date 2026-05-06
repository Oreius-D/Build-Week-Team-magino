using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    Dictionary<PoolId, List<GameObject>> pools = new Dictionary<PoolId, List<GameObject>>();
    [SerializeField] public Transform Player;
    protected override bool ShouldBeDestoyOnLoad() => true;
    public void CreatePool(PoolSettings poolSetting)
    {
        var list = new List<GameObject>();
        for (int i = 0; i < poolSetting.numPerPrefab; i++)
        {
            foreach(var prefab in poolSetting.Prefabs)
            {
                var obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                list.Add(obj);
            }
        }
        pools.Add(poolSetting.Id, list);
    }

    public GameObject GetItemPool(PoolSettings poolType)
    {
        if (pools.TryGetValue(poolType.Id, out List<GameObject> list))
        {
            int index = Random.Range(0, list.Count);
            var obj = list[index];
            obj.SetActive(true);
            list.RemoveAt(index);
            return obj;
        }
        Debug.LogWarning($"Non ci sono ItemPool nella pool di questo tipo: {poolType.Id}");
        return null;
    }
    public void ReturnToPool(PoolId id, GameObject obj)
    {
        if (pools.TryGetValue(id, out List<GameObject> list))
        {
            obj.SetActive(false);
            list.Add(obj);
        }

    }
    public void DestroyPool(PoolId id)
    {
        if(pools.TryGetValue(id,out List<GameObject> list))
        {
            foreach (var obj in list) Destroy(obj);
            list.Clear();
            pools.Remove(id);
        }
    }
}
