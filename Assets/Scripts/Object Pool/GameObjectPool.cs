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
    private GameObjectPoolOwner owner;

    /// <summary>
    /// Current size of the pool
    /// </summary>
    public int PoolSize { get; private set; }

    public class GameObjectPoolOwner
    {
        private readonly GameObjectPool owner;

        public GameObjectPoolOwner(GameObjectPool owner)
        {
            this.owner = owner;
        }


        public void ReturnToPool(GameObject poolObject)
        {
            owner.ReturnToPool(poolObject);
        }
        /// <summary>
        /// return object to pool, use by GameObjectPoolSpy
        /// </summary>
        public void UnsubscribedFromPool(GameObject poolObject)
        {
            owner.UnsubscribedFromPool(poolObject);
        }

    }


    private void Awake()
    {
        PoolSize = maxObjectInPool;
        owner = new GameObjectPoolOwner(this);

        if (prewarm)
        {
            int size = maxObjectInPool - subscribers.Count;
            for (int i = 0; i < size; i++)
            {
                CreateInstance().gameObject.SetActive(false);
            }

        }
    }

    private GameObject CreateInstance()
    {
        GameObject instance = Instantiate(prefab);
        instance.gameObject.AddComponent<GameObjectPoolSyp>().Owner = owner;
        subscribers.Add(instance);
        return instance;
    }

    /// <summary>
    /// Get object from pool or create new one
    /// </summary>
    public GameObject GetOrCreate()
    {
        GameObject instance = null;

        if (pool.Count == 0)
        {
            if (ShouldExtend()) PoolSize *= 2;

            if (PoolSize > subscribers.Count || allowExceedPoolSize)
            {
                instance = CreateInstance();
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

    /// <summary>
    /// return object to pool, use by ObjectPoolSpy
    /// </summary>
    private void ReturnToPool(GameObject poolObject)
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

    private void UnsubscribedFromPool(GameObject poolObject)
    {
        subscribers.Remove(poolObject);
    }


    private bool ShouldExtend()
    {
        return dynamicExtend && PoolSize <= subscribers.Count;
    }
}
