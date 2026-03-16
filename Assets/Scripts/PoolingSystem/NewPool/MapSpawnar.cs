using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnar : MonoBehaviour
{
    [SerializeField] private PoolSettings segments;
    [SerializeField] private PoolSettings coin;


    [SerializeField] private float lengthSection = 20f;
    [SerializeField] private int sectionOnScreen = 2;

    [SerializeField] private float maxDistance = 100f;

    private float spawnZ = 0f;
    private void Start()
    {
        SpawnSection();
        ActiveSection();

    }
    private void Update()
    {
        if (Pool.Instance.Player.position.z > spawnZ - (sectionOnScreen * lengthSection))
        {
            ActiveSection();
        }

        if (Pool.Instance.Player.position.z > maxDistance)
        {
            ResetMap();
        }
    }
    public void SpawnSection()
    {
        Pool.Instance.CreatePool(segments);
        Pool.Instance.CreatePool(coin);
    }
    public void ActiveSection()
    {
        var obj = Pool.Instance.GetItemPool(PoolId.Map);
        obj.transform.position = Vector3.forward * spawnZ;
        spawnZ += lengthSection;
        obj.GetComponent<Segment>().SpawnCoin();
    }
    public void ResetMap()
    {
        foreach (Transform child in Pool.Instance.transform)
        {
            Segment segment = child.GetComponent<Segment>();
            if (segment!=null&& child.gameObject.activeInHierarchy)
            {
                Pool.Instance.ReturnToPool(segments.Id, child.gameObject);
            }
        }
        spawnZ = 0;
        Pool.Instance.Player.position = new Vector3(Pool.Instance.Player.position.x, Pool.Instance.Player.position.y, spawnZ);

        for (int i = 0; i < sectionOnScreen; i++)
        {
            ActiveSection();
        }
    }
}
