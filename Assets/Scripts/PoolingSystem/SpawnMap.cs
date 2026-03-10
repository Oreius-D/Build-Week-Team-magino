using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lengthSection=20f;
    [SerializeField] private int sectionOnScreen=2;
    [SerializeField] private float maxDistance;

    private float spawnZ;
    private void Start()
    {
        for (int i = 0; i < sectionOnScreen; i++)
        {
            SpawnSection();
        }
    }
    private void Update()
    {
        if (player.position.z > spawnZ - (sectionOnScreen * lengthSection))
        {
            SpawnSection();
        }

        if (player.position.z > maxDistance)
        {
            ResetMap();
        }
    }

    private void SpawnSection()
    {
        var section = MapGenerator.Instance.TakeFromPool();
        section.transform.position = Vector3.forward * spawnZ;
        spawnZ += lengthSection;
    }
    private void ResetMap()
    {
        foreach (Transform child in MapGenerator.Instance.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                MapGenerator.Instance.ReturnToPool(child.gameObject);
            }
        }

        spawnZ = 0;
        player.position = new Vector3(player.position.x, player.position.y, 0);

        for (int i = 0; i < sectionOnScreen; i++)
        {
            SpawnSection();
        }
    }
}
