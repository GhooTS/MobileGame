using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int maxObjectInPool = 15;
    public bool dynamicExtend;
    public bool allowExcedingPoolSize;
    public bool preWarm = false;
    private readonly Stack<GameObject> pool = new Stack<GameObject>();
    /// <summary>
    /// All objects craeted by this pool
    /// </summary>
    private readonly List<GameObject> subscribers = new List<GameObject>();

    public int PoolSize { get; private set; }

    private void Awake()
    {
        PoolSize = maxObjectInPool;

        if (preWarm)
        {
            int size = maxObjectInPool - subscribers.Count;
            GameObject instance;
            for (int i = 0; i < size; i++)
            {
                instance = Instantiate(prefab);
                instance.gameObject.AddComponent<GameObjectPoolSyp>().owner = this;
                subscribers.Add(instance);
                instance.gameObject.SetActive(false);
            }

        }
    }


    public GameObject GetOrCreate()
    {
        GameObject instance = null;

        if (pool.Count == 0)
        {
            if (ShoudExtend()) PoolSize *= 2;

            if (PoolSize > subscribers.Count || allowExcedingPoolSize)
            {
                instance = Instantiate(prefab);
                instance.gameObject.AddComponent<GameObjectPoolSyp>().owner = this;
                subscribers.Add(instance);
            }
        }
        else
        {
            instance = pool.Pop();
            instance.gameObject.SetActive(true);
        }

        return instance;
    }

    public void ReturnToPool(GameObject poolObject)
    {
        if (allowExcedingPoolSize && subscribers.Count >= PoolSize)
        {
            Destroy(poolObject.gameObject);
        }
        else
        {
            pool.Push(poolObject);
        }
    }

    public void UnsubscribedFromPool(GameObject poolObject)
    {
        subscribers.Remove(poolObject);
    }


    public void Clear()
    {
        pool.Clear();
        subscribers.Clear();
    }

    public void ClearAndDestroy()
    {
        foreach (var subscriber in subscribers)
        {
            Destroy(subscriber.gameObject);
        }
        Clear();
    }

    private bool ShoudExtend()
    {
        return dynamicExtend && PoolSize <= subscribers.Count;
    }
}
