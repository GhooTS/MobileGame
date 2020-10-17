using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    public CoreCellDynamicCollection activeCells;
    public CoreCellDynamicCollection deactiveCells;
    [Header("Events")]
    public UnityEvent onGameStarting;
    public UnityEvent onGameStarted;
    public UnityEvent onGameEnding;
    public UnityEvent onGameEnded;

    public bool IsPlaying { get; private set; } = false;

    private void Update()
    {
        if(IsPlaying && activeCells.Count == 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        onGameStarting?.Invoke();
        ResetPlayerLifes();
        onGameStarted?.Invoke();
        IsPlaying = true;
    }

    private void ResetPlayerLifes()
    {
        for (int i = deactiveCells.Count - 1; i >= 0; i--)
        {
            deactiveCells[i].Repair();
        }
    }

    [ContextMenu("EndGame")]
    private void EndGame()
    {
        onGameEnding?.Invoke();
        IsPlaying = false;
        onGameEnded?.Invoke();
    }

}
