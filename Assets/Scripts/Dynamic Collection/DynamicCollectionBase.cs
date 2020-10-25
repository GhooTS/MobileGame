using UnityEngine;

public abstract class DynamicCollectionBase : ScriptableObject
{
    /// <summary>
    /// Use this method to subscribe to the collection
    /// </summary>
    /// <param name="gameObject">Subscriber or object contains subscriber</param>
    /// <returns>returns whether subscription was successful</returns>
    public abstract bool Subscribe(GameObject gameObject);
    /// <summary>
    /// Use this method to unsubscribe from the collection
    /// </summary>
    /// <param name="gameObject">Subscriber or object contains subscriber</param>
    /// <returns>returns whether unsubscription was successful</returns>
    public abstract bool Unsubscribe(GameObject gameObject);
}
