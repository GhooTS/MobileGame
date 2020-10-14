using UnityEngine;
using UnityEngine.Events;

public class BallKeeper : MonoBehaviour
{
    public Bounds moveBounds;
    public Ball ball;
    public Transform ballSpawnPoint;

    public UnityEvent onBallReseted;

    private void Update()
    {
        if (!moveBounds.Contains(ball.transform.position))
        {
            ball.SetArmed(true);
            ball.Velocity = Vector3.zero;
            ball.transform.position = ballSpawnPoint.position;
            onBallReseted?.Invoke();
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(moveBounds.center, moveBounds.size);
    }
}
