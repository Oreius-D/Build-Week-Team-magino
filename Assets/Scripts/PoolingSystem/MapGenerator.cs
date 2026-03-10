using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Singleton<MapGenerator>
{
    [SerializeField] private GameObject[] sectionsMap;

    List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        CreateObject();
    }
    public void CreateObject()
    {
        foreach (GameObject section in sectionsMap)
        {
            var obj = Instantiate(section, transform);
            obj.SetActive(false);
            pool.Add(obj);

        }
    }
    public GameObject TakeFromPool()
    {
        if (pool.Count == 0)
        {
            Debug.LogError("non esiste niente nella pool");
            return null;
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
