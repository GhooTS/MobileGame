using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public abstract class ObjectPool<T,SpyT> : MonoBehaviour
    where T : MonoBehaviour
    where SpyT : ObjectPoolSpy<T,SpyT>
{
    public T prefab;
    public int maxObjectInPool = 15;
    public bool dynamicExtend;
    public bool allowExcedingPoolSize;
    public bool preWarm = false;
    private readonly Stack<T> pool = new Stack<T>();
    /// <summary>
    /// All objects craeted by this pool
    /// </summary>
    private readonly List<T> subscribers = new List<T>();

    public int PoolSize { get; private set; }

    private void Awake()
    {
        PoolSize = maxObjectInPool;

        if (preWarm)
        {
            int size = maxObjectInPool - subscribers.Count;
            T instance;
            for (int i = 0; i < size; i++)
            {
                instance = Instantiate(prefab);
                instance.gameObject.AddComponent<SpyT>().owner = this;
                subscribers.Add(instance);
                instance.gameObject.SetActive(false);
            }

        }
    }


    public T GetOrCreate()
    {
        T instance = null;

        if (pool.Count == 0)
        {
            if (ShoudExtend()) PoolSize *= 2;

            if (PoolSize > subscribers.Count || allowExcedingPoolSize)
            {
                instance = Instantiate(prefab);
                instance.gameObject.AddComponent<SpyT>().owner = this;
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

    public void ReturnToPool(T poolObject)
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

    public void UnsubscribedFromPool(T poolObject)
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
