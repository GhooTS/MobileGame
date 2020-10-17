using UnityEngine;
using UnityEngine.Events;

public class BallKeeper : MonoBehaviour
{
    public Bounds moveBounds;
    public Ball ball;
    public BallController ballController;
    public Transform ballSpawnPoint;

    public UnityEvent onBallReseted;

    private void Start()
    {
        ballController = FindObjectOfType<BallController>();
    }

    private void Update()
    {
        KeepBallInBounds();
    }

    private void KeepBallInBounds()
    {
        if (!moveBounds.Contains(ball.transform.position))
        {
            RestardBall();
            onBallReseted?.Invoke();
        }
    }

    public void RestardBall()
    {
        ballController.FreeBall(false);
        ball.SetArmed(true);
        ball.Velocity = Vector3.zero;
        ball.transform.position = ballSpawnPoint.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(moveBounds.center, moveBounds.size);
    }
}
