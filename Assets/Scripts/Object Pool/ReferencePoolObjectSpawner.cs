using UnityEngine;

public class ReferencePoolObjectSpawner : MonoBehaviour
{
    public GameObjectPoolReference poolReference;

    public void SpawnFromPool()
    {
        var instance = poolReference.Instance.GetOrCreate();
        instance.transform.position = transform.position;
    }
}
