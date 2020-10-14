using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class TowPointLineConnection : MonoBehaviour
{
    public Transform start;
    public Transform end;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
    }

    private void LateUpdate()
    {
        if(start != null && end != null)
        {
            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, end.position);
        }
    }
}
