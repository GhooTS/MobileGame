using UnityEngine;
using UnityEngine.Events;

public class CoreCell : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onRepaired;
    public UnityEvent onDestroyed;
    public bool Destroyed { get; private set; } = false;


    public void Repair()
    {
        Destroyed = false;
        onRepaired?.Invoke();
    }

    public void DestroyCell()
    {
        Destroyed = true;
        onDestroyed?.Invoke();
    }
}
