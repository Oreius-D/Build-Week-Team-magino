using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Jobs.LowLevel.Unsafe;

public class TestingPool : Singleton<TestingPool>
{
    [SerializeField] private PoolData[] poolDataInspector;
    private Dictionary<PoolId, List<GameObject>> poolData;
    protected override void Awake()
    {
        base.Awake();
        poolData = new Dictionary<PoolId, List<GameObject>>();
        CreateObject(2);
    }

    private void CreateObject(int numPerPrefab)
    {
        //List<GameObject> pi = new List<GameObject>();

        //for (int i = 0; i < numPerPrefab; i++)
        //{
        //    foreach (var dataPool in poolDataInspector)
        //    {
        //        foreach(var objPrefab in dataPool.Objects)
        //        {
        //            var obj = Instantiate(objPrefab,transform);
        //            obj.SetActive(false);
        //            pi.Add(obj);
        //        }
        //        poolData.Add(dataPool.Id, pi);
        //    }
        //    pi.Clear();
        //}
     
    }
    public void ReturntoPool(PoolId id, GameObject obj)
    {
        if(poolData.TryGetValue(id, out List<GameObject> pi))
        {
            obj.SetActive(false);
            pi.Add(obj);
        }

    }
}
