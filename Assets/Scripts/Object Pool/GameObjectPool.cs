using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int maxObjectInPool = 15;
    [Tooltip("Allow pool to double its size when running out of space")]
    public bool dynamicExtend;
    [Tooltip("Allow pool to created object even if the pool is full, these objects will be destroyed, when returning to the pool")]
    public bool allowExceedPoolSize;
    [Tooltip("Whether objects should be created on awake")]
    public bool prewarm = false;
    private readonly Stack<GameObject> pool = new Stack<GameObject>();
    /// <summary>
    /// All objects craeted by this pool
    /// </summary>
    private readonly List<GameObject> subscribers = new List<GameObject>();

    /// <summary>
    /// Current size of the pool
    /// </summary>
    public int PoolSize { get; private set; }

    private void Awake()
    {
        PoolSize = maxObjectInPool;

        if (prewarm)
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

    /// <summary>
    /// Get object from pool or create new one
    /// </summary>
    public GameObject GetOrCreate()
    {
        GameObject instance = null;

        if (pool.Count == 0)
        {
            if (ShoudExtend()) PoolSize *= 2;

            if (PoolSize > subscribers.Count || allowExceedPoolSize)
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

    /// <summary>
    /// return object to pool, use by ObjectPoolSpy
    /// </summary>
    public void ReturnToPool(GameObject poolObject)
    {
        if (allowExceedPoolSize && subscribers.Count >= PoolSize)
        {
            Destroy(poolObject.gameObject);
        }
        else
        {
            pool.Push(poolObject);
        }
    }
    /// <summary>
    /// return object to pool, use by GameObjectPoolSpy
    /// </summary>
    public void UnsubscribedFromPool(GameObject poolObject)
    {
        subscribers.Remove(poolObject);
    }

    /// <summary>
    /// Collect all active object and put them back to the pool 
    /// </summary>
    public void ClearAndDestroy()
    {
        foreach (var subscriber in subscribers)
        {
            Destroy(subscriber.gameObject);
        }
        Clear();
    }

    private void Clear()
    {
        pool.Clear();
        subscribers.Clear();
    }

    private bool ShoudExtend()
    {
        return dynamicExtend && PoolSize <= subscribers.Count;
    }
}
