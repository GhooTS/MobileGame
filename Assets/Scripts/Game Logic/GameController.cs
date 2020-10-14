using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CoreCellDynamicCollection activeCells;
    [Header("Events")]
    public UnityEvent onGameStarting;
    public UnityEvent onGameStarted;
    public UnityEvent onGameEnding;
    public UnityEvent onGameEnded;

    private void Awake()
    {
        StartGame();
    }
    private void Update()
    {
        if(activeCells.Count == 0)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        onGameStarting?.Invoke();
        onGameStarted?.Invoke();
    }

    private void EndGame()
    {
        onGameEnding?.Invoke();
        onGameEnded?.Invoke();
    }

}
