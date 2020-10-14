using UnityEngine;

public abstract class DynamicCollectionBase : ScriptableObject
{
    public abstract bool Subscribe(GameObject gameObject);
    public abstract bool Unsubscribe(GameObject gameObject);
}
