using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public UnityEvent onHitCore;
    public UnityEvent onDestroy;
    public Vector3 Velocity { get; private set; } = Vector3.zero;
    private Vector3 targetPosition;

    private void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }


    public Asteroid SetTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        return this;
    }

    public Asteroid SetSpeed(float speed)
    {
        var distanceVector = targetPosition - transform.position;
        var x = speed * Mathf.Sign(distanceVector.x);
        var z = distanceVector.z / Mathf.Abs(distanceVector.x / speed);
        Velocity = new Vector3(x, 0, z);

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.TryGetComponent(out Ball ball) && ball.Armed)
        {
            onDestroy?.Invoke();
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Core") && other.TryGetComponent(out CoreCell cell))
        {
            onHitCore?.Invoke();
            cell.DestroyCell();
            gameObject.SetActive(false);
        }
    }
}
