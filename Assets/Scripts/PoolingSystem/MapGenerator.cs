using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] private GameObject[] sectionsMap;
    [SerializeField] private int numPrefabPerSection=2;

    private List<GameObject> pool = new List<GameObject>();
    public List<GameObject> Pool {get =>pool;}

    protected override void Awake()
    {
        base.Awake();
        CreateObject(numPrefabPerSection);
    }
    public void CreateObject(int numPrefab)
    {
        for (int i=0;i<numPrefab; i++)
        {
            foreach(GameObject section in sectionsMap)
            {
                var obj = Instantiate(section, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }
    public GameObject TakeFromPool()
    {
        if (pool.Count == 0)
        {
            CreateObject(1);
        }

        int randomIndex = Random.Range(0, pool.Count);

        GameObject obj = pool[randomIndex];

        pool.RemoveAt(randomIndex);

        obj.SetActive(true);
        return obj;
       
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }


}
