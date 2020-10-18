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
        if (Destroyed == false) return;

        Destroyed = false;
        onRepaired?.Invoke();
    }

    public void DestroyCell()
    {
        if (Destroyed) return;

        Destroyed = true;
        onDestroyed?.Invoke();
    }
}
