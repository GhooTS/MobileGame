using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationVelocity;

    private void Update()
    {
        transform.Rotate(rotationVelocity * Time.deltaTime);
    }
}
