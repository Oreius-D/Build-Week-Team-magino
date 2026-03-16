using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    Dictionary<PoolId, List<GameObject>> pools = new Dictionary<PoolId, List<GameObject>>();
    [SerializeField] public Transform Player;

    public void CreatePool(PoolSettings poolSetting)
    {
        //var list = new List<GameObject>();
        //for (int i = 0; i < poolSetting.DefaultSize; i++)
        //{
        //    var obj = Instantiate(poolSetting.Prefab, transform);
        //    obj.SetActive(false);
        //    list.Add(obj);
        //}    
        //pools.Add(poolSetting.Id, list);
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

    public GameObject GetItemPool(PoolId id)
    {
        if (pools.TryGetValue(id, out List<GameObject> list))
        {
            Debug.Log(list.Count + "lista");
            int index = Random.Range(0, list.Count);
            Debug.Log(index + "index");
            var obj = list[index];
            obj.SetActive(true);
            list.RemoveAt(index);
            return obj;
        }
        return null;
    }
    public void ReturnToPool(PoolId id, GameObject obj)
    {
        if (pools.TryGetValue(id, out List<GameObject> list))
        {
            Debug.Log("sono stata richiamata ReturnToPool");
            obj.SetActive(false);
            list.Add(obj);
        }

    }
}
