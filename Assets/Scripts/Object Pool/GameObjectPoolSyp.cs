using UnityEngine;

public class GameObjectPoolSyp : MonoBehaviour
{
    public GameObjectPool.GameObjectPoolOwner Owner 
    {
        set
        {
            if(owner != null && owner != value)
            {
                owner.UnsubscribedFromPool(gameObject);
            }

            owner = value;
        }
    }
    private GameObjectPool.GameObjectPoolOwner owner;

    private void OnDisable()
    {
        owner.ReturnToPool(gameObject);
    }

    private void OnDestroy()
    {
        owner.UnsubscribedFromPool(gameObject);
    }
}
