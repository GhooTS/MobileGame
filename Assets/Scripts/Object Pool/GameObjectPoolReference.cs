using UnityEngine;


[CreateAssetMenu(menuName = "Object Pool/GameObject pool reference")]
public class GameObjectPoolReference : ScriptableObject
{
    [SerializeField]
    private GameObjectPool objectPoolPrefab = default;
    private GameObjectPool instance;
    public GameObjectPool Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(objectPoolPrefab, Vector3.zero, Quaternion.identity);
            }

            return instance;
        }
    }


}
