using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnar : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PoolSettings poolSettings;

    [SerializeField] private float lengthSection = 20f;
    [SerializeField] private int sectionOnScreen = 2;

    [SerializeField] private float maxDistance=100f;

    private float spawnZ=0f;
    private void Start()
    {
        SpawnSection();
        ActiveSection();
        ActiveSection();

    }
    private void Update()
    {
        if (player.position.z > spawnZ - (sectionOnScreen * lengthSection))
        {
            ActiveSection();
        }

        if (player.position.z > maxDistance)
        {
            ResetMap();
        }
    }
    public void SpawnSection()
    {
        Pool.Instance.CreatePool(poolSettings);
    }
    public void ActiveSection()
    {
        var obj = Pool.Instance.GetItemPool(poolSettings.Id);
        obj.transform.position = Vector3.forward * spawnZ;
        spawnZ += lengthSection;
       
    }
    public void ResetMap()
    {
        Debug.Log("ResetMap viene richiamata");
        foreach (Transform child in Pool.Instance.transform)
        {
            Debug.Log("sono tutti gli oggetti dentro l'oggetto");
            if (child.gameObject.activeInHierarchy)
            {
                Debug.Log("sono tutti gli oggetti attivi che sono tornati nella pool");
               Pool.Instance.ReturnToPool(poolSettings.Id, child.gameObject);
            }
        }

        spawnZ = 0;
        player.position = new Vector3(player.position.x, player.position.y, spawnZ);

        for (int i = 0; i < sectionOnScreen; i++)
        {
            ActiveSection();
        }
    }
}
