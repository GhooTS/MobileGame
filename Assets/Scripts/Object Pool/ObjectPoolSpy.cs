using UnityEngine;

public class ObjectPoolSpy<T,SpyT> : MonoBehaviour
    where T : MonoBehaviour
    where SpyT : ObjectPoolSpy<T,SpyT>
{
    public  ObjectPool<T,SpyT> owner;
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