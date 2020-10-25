using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public abstract class ObjectPool<T,SpyT> : MonoBehaviour
    where T : Component
    where SpyT : ObjectPoolSpy<T,SpyT>
{
    public T prefab;
    public int maxObjectInPool = 15;
    [Tooltip("Allow pool to double its size when running out of space")]
    public bool dynamicExtend;
    [Tooltip("Allow pool to created object even if the pool is full, these objects will be destroyed, when returning to the pool")]
    public bool allowExceedPoolSize;
    [Tooltip("Whether objects should be created on awake")]
    public bool prewarm = false;
    /// <summary>
    /// pool of free objects
    /// </summary>
    private readonly Stack<T> pool = new Stack<T>();
    /// <summary>
    /// All objects craeted by this pool or belongs to this object pool
    /// </summary>
    private readonly List<T> subscribers = new List<T>();

    /// <summary>
    /// Current size of the pool
    /// </summary>
    public int PoolSize { get; private set; }

    private ObjectPoolOwner owner;

    public class ObjectPoolOwner
    {
        private readonly ObjectPool<T, SpyT> owner;

        public ObjectPoolOwner(ObjectPool<T, SpyT> owner)
        {
            this.owner = owner;
        }

        public void ReturnToPool(T poolObject)
        {
            owner.ReturnToPool(poolObject);
        }

        public bool UnsubscribedFromPool(T poolObject)
        {
            return owner.UnsubscribedFromPool(poolObject);
        }
    }

    private void Awake()
    {
        owner = new ObjectPoolOwner(this);
        PoolSize = maxObjectInPool;

        if (prewarm)
        {
            int size = maxObjectInPool - subscribers.Count;
            for (int i = 0; i < size; i++)
            {
                CreteaInstance().gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Get object from pool or create new one
    /// </summary>
    public T GetOrCreate()
    {
        return GetOrCreate(Vector3.zero, Quaternion.identity);
    }

    /// <summary>
    /// Get object from pool or create new one
    /// </summary>
    public T GetOrCreate(Vector3 position,Quaternion rotation)
    {
        T instance = null;

        if (pool.Count == 0)
        {
            if (ShouldExtend()) PoolSize *= 2;

            if (PoolSize > subscribers.Count || allowExceedPoolSize)
            {
                instance = CreteaInstance();
            }
        }
        else
        {
            instance = pool.Pop();
            instance.gameObject.SetActive(true);
            instance.transform.position = position;
            instance.transform.rotation = rotation;
        }

        return instance;
    }
    


    /// <summary>
    /// Collect all active object and put them back to the pool 
    /// </summary>
    public void CollecteAll()
    {
        for (int i = subscribers.Count - 1; i >= 0; i--)
        {
            if(subscribers[i] == null)
            {
                subscribers.RemoveAt(i);
                continue;
            }

            if (subscribers[i].gameObject.activeSelf)
            {
                subscribers[i].gameObject.SetActive(false);
            }
        }
    }


   

    /// <summary>
    /// Clear pool and destroy objects belongs to this objectPool
    /// </summary>
    public void ClearAndDestroy()
    {
        foreach (var subscriber in subscribers)
        {
            Destroy(subscriber.gameObject);
        }
        Clear();
    }


    private T CreteaInstance()
    {
        T instance = Instantiate(prefab);
        instance.gameObject.AddComponent<SpyT>().Owner = owner;
        subscribers.Add(instance);
        return instance;
    }

    /// <summary>
    /// return object to pool, use by ObjectPoolSpy
    /// </summary>
    private void ReturnToPool(T poolObject)
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

    private bool UnsubscribedFromPool(T poolObject)
    {
        return subscribers.Remove(poolObject);
    }

    private void Clear()
    {
        pool.Clear();
        subscribers.Clear();
    }

    private bool ShouldExtend()
    {
        return dynamicExtend && PoolSize <= subscribers.Count;
    }
}
