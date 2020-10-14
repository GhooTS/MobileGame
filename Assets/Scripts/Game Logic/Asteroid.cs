using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    public CoreCellDynamicCollection activeCells;
    public UnityEvent onHitCore;
    public UnityEvent onDestroy;
    public Vector3 Velocity { get; private set; } = Vector3.zero;

    private void Update()
    {
        transform.position += Velocity * Time.deltaTime;
    }

    public void SetSpeed(float speed)
    {


        if (activeCells.Count != 0)
        {
            var distanceVector = activeCells[Random.Range(0, activeCells.Count - 1)].transform.position - transform.position;

            var x = speed * Mathf.Sign(distanceVector.x);
            var z = distanceVector.z / Mathf.Abs(distanceVector.x / speed);
            Velocity = new Vector3(x, 0, z);
        }
        else
        {
            Velocity = Vector3.zero;
        }
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
