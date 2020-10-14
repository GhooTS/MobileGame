using UnityEngine;

public class GameObjectPoolSyp : MonoBehaviour
{
    public GameObjectPool owner;

    private void OnDisable()
    {
        owner.ReturnToPool(gameObject);
    }

    private void OnDestroy()
    {
        owner.UnsubscribedFromPool(gameObject);
    }
}
