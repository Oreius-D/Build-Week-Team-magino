using Cinemachine;
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

    CharacterController cc;
    [SerializeField] CinemachineVirtualCamera vCam;

    private float spawnZ = 0f;
    private void Start()
    {
        SpawnPool();
        ActiveSection();
        cc = Pool.Instance.Player.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Pool.Instance.Player.position.z > spawnZ - (sectionOnScreen * lengthSection))
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
        Pool.Instance.CreatePool(segments);
        Pool.Instance.CreatePool(coin);
    }
    public void ActiveSection()
    {
        var obj = Pool.Instance.GetItemPool(segments.Id);
        obj.transform.position = Vector3.forward * spawnZ;
        spawnZ += lengthSection;
        obj.GetComponent<Segment>().SpawnCoin();
    }
    public void ResetMap()
    {
        foreach (Transform child in Pool.Instance.transform)
        {
           // Segment segment = child.GetComponent<Segment>();
            PooledObject itemPool= child.GetComponentInChildren<PooledObject>();
            if (itemPool!= null && child.gameObject.activeInHierarchy)
            {
                Debug.Log("esiste Oggetto ed è attivo");
                Pool.Instance.ReturnToPool(itemPool.PoolFrom.Id, child.gameObject);
            }
        }
        spawnZ = 0;
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
        Vector3 newPosition = new Vector3(Pool.Instance.Player.position.x, Pool.Instance.Player.position.y, spawnZ);
        Pool.Instance.Player.position = newPosition;
        WarpCam(newPosition,oldPosition);
        if (cc != null)
        {
            cc.enabled = true;
        }
    }
}
