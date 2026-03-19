using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSpawnar : MonoBehaviour
{
    [Header("TypeMap")]
    [SerializeField] private PoolSettings forestMap;
    [SerializeField] private PoolSettings desertMap;

    [Header("ChangeMap")]
    private PoolSettings currentMap;
    private PoolSettings previusMap;
    private bool isChanging=false;

    [SerializeField] private PoolSettings coinPool;

    [Header("Section Settings")]
    [SerializeField] private float lengthSection = 50f;
    [SerializeField] private int sectionOnScreen = 2;

    [Header("Milesotne Settings")]
    [SerializeField] private float currentMilestone=100f;
    [SerializeField] private float gapMilestone=100f;
    [SerializeField] private float moltiplicationGap= 1.5f;

    [Header("Reset Settings")]
    [SerializeField] private float maxDistance = 100f;
    CharacterController cc;
    [SerializeField] CinemachineVirtualCamera vCam;
    private float spawnZMap = 0f;
    private float spawnZPlayer = -10f;

    private void Start()
    {
        SpawnPool(forestMap);
        SpawnPool(coinPool);
        currentMap = forestMap;
        for (int i = 0; i < sectionOnScreen; i++)
        {
            ActiveSection();
        }
        cc = Pool.Instance.Player.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Pool.Instance.Player.position.z > spawnZMap - (sectionOnScreen * lengthSection))
        {
            ActiveSection();
        }
        //if (Pool.Instance.Player.position.z >= 100 && !isChanging)
        //{
        //    previusMap= currentMap;
        //    currentMap = desertMap;
        //    SpawnPool(currentMap);
        //    isChanging = true;
        //}
       // ChangeMap(Pool.Instance.Player.position.z, ref currentMilestone, gapMilestone, moltiplicationGap);
        if (Pool.Instance.Player.position.z >= maxDistance)
        {
            ResetMap();
        }
    }
    public void SpawnPool(PoolSettings pool)
    {
        Pool.Instance.CreatePool(pool);
    }
    public void ActiveSection()
    {
        var obj = Pool.Instance.GetItemPool(currentMap);
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

        DestroyMap();
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
    public void ChangeMap(float distanceTravelled ,ref float milestone,float nextGap, float moltiplicationGap)
    {
        if (distanceTravelled >= milestone)
        {
            previusMap = currentMap;
            currentMap = desertMap;
            SpawnPool(currentMap);
            isChanging = true;
            milestone += nextGap*moltiplicationGap;
        }
    }
    private void DestroyMap()
    {
        if (isChanging&&previusMap!=null)
        {
            isChanging = false;
            Pool.Instance.DestroyPool(previusMap.Id);
            previusMap = null;
        }
    }
}
