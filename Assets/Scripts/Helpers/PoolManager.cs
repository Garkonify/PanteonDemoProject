using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] List<Pool> startingPools;
    Dictionary<PoolableSO, Queue<GameObject>> pools;

    private void Start()
    {
        pools = new Dictionary<PoolableSO, Queue<GameObject>>();
        foreach (var pool in startingPools)
        {
            AddPool(pool);
        }
    }
    void AddPool(Pool pool)
    {
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        for (int i = 0; i < pool.defaultPoolCount; i++)
        {
            GameObject go = Instantiate(pool.poolType.prefab);
            go.SetActive(false);
            tempQueue.Enqueue(go);
        }
        pools.Add(pool.poolType, tempQueue);
    }

    public GameObject Get(PoolableSO poolType)
    {
        if (pools.ContainsKey(poolType))
        {
            if (pools[poolType].Count > 0)
            {
                return pools[poolType].Dequeue();
            }
            else { return Instantiate(poolType.prefab); }
        }
        else
        {
            AddPool(new Pool(poolType, 5));
            return Get(poolType);
        }
    }
    public void Give(GameObject go)
    {
        PoolableSO poolable = go.GetComponent<IPoolable>().GivePoolData();
        pools[poolable].Enqueue(go);
    }

}
[System.Serializable]
public class Pool
{
    public PoolableSO poolType;
    public int defaultPoolCount;
    public Pool(PoolableSO poolType, int defaultPoolCount)
    {
        this.poolType = poolType;
        this.defaultPoolCount = defaultPoolCount;
    }
}

