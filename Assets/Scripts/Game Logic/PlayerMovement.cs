using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    IPlayerControls playerControls;
    public float speed = 10f;
    public float boardSize;
    public float playerSize;
    private float moveDistance = 11f;

    private void Start()
    {
        playerControls = GetComponent<IPlayerControls>();

        moveDistance = (boardSize - playerSize) / 2f;
    }


    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.z += playerControls.GetVerticalInput() * speed * Time.deltaTime;
        newPosition.z = Mathf.Clamp(newPosition.z,-moveDistance,moveDistance);

        transform.position = newPosition;
    }
}
