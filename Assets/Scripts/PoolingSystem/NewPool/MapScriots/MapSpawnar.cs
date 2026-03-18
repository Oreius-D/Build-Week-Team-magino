using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawnar : MonoBehaviour
{
    [SerializeField] private PoolSettings forestMap;
    [SerializeField] private PoolSettings coinPool;


    [SerializeField] private float lengthSection = 20f;
    [SerializeField] private int sectionOnScreen = 2;

    [SerializeField] private float maxDistance = 100f;

    CharacterController cc;
    [SerializeField] CinemachineVirtualCamera vCam;

    private float spawnZMap = 0f;
    private float spawnZPlayer = -10f;
    private void Start()
    {
        SpawnPool();
        ActiveSection();
        cc = Pool.Instance.Player.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Pool.Instance.Player.position.z > spawnZMap - (sectionOnScreen * lengthSection))
        {
            ActiveSection();
        }

        if (Pool.Instance.Player.position.z >= maxDistance)
        {
            ResetMap();
        }
    }
    public void SpawnPool()
    {
        Pool.Instance.CreatePool(forestMap);
        Pool.Instance.CreatePool(coinPool);
    }
    public void ActiveSection()
    {
        var obj = Pool.Instance.GetItemPool(forestMap);
        obj.transform.position = Vector3.forward * spawnZMap;
        spawnZMap += lengthSection;
        obj.GetComponent<Segment>().SpawnCoin();
    }
    public void ResetMap()
    {
        foreach (Transform child in Pool.Instance.transform)
        {
            PooledObject itemPool= child.GetComponent<PooledObject>();
            if (itemPool!= null && child.gameObject.activeInHierarchy)
            {
                Pool.Instance.ReturnToPool(itemPool.PoolFrom.Id, child.gameObject);
            }
        }
        spawnZMap = 0;
        ResetPlayer();
        for (int i = 0; i < sectionOnScreen; i++)
        {
            ActiveSection();
        }
    }
    private void WarpCam(Vector3 newPostion, Vector3 oldPosition)
    {
        Vector3 delta= newPostion - oldPosition;
        vCam.OnTargetObjectWarped(Pool.Instance.Player, delta);

    }
    private void ResetPlayer()
    {
        if (cc != null)
        {
            cc.enabled = false;
        }
        Vector3 oldPosition = Pool.Instance.Player.position;
        Vector3 newPosition = new Vector3(Pool.Instance.Player.position.x, Pool.Instance.Player.position.y, spawnZPlayer);
        Pool.Instance.Player.position = newPosition;
        WarpCam(newPosition,oldPosition);
        if (cc != null)
        {
            cc.enabled = true;
        }
    }
}
