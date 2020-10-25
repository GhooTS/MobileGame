using UnityEngine;

public class ObjectPoolSpy<T,SpyT> : MonoBehaviour
    where T : Component
    where SpyT : ObjectPoolSpy<T,SpyT>
{
    public ObjectPool<T, SpyT>.ObjectPoolOwner Owner 
    {
        set 
        {
            //Make sure to unsubscirber before chanaging the owner
            if(owner != null && owner != value && poolObject != null)
            {
                owner.UnsubscribedFromPool(poolObject);
            }

            owner = value;
        }
    }
    private ObjectPool<T, SpyT>.ObjectPoolOwner owner;
    private T poolObject;

    private void Start()
    {
        if(poolObject == null) poolObject = GetComponent<T>();
    }

    private void OnDisable()
    {
        if(poolObject == null)
        {
            poolObject = GetComponent<T>();
        }
        owner.ReturnToPool(poolObject);
    }

    private void OnDestroy()
    {
        owner.UnsubscribedFromPool(poolObject);
    }
}