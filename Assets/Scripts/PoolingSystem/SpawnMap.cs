using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float lengthSection=20f;
    [SerializeField] private int sectionOnScreen=2;

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
    }

    private void SpawnSection()
    {
        var section = MapGenerator.Instance.TakeFromPool();
        //if (spawnZ > 100)
        //{
        //    spawnZ = 0;
        //    player.position = Vector3.forward * spawnZ;
        //}
        section.transform.position = Vector3.forward * spawnZ;
        spawnZ += lengthSection;
    }
}
