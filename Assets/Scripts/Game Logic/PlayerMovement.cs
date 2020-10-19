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

        //Calculate player move limits
        moveDistance = (boardSize - playerSize) / 2f;
    }


    private void Update()
    {
        Vector3 newPosition = transform.position;
        //Get player input
        newPosition.z += playerControls.GetVerticalInput() * speed * Time.deltaTime;
        //Limit player movement
        newPosition.z = Mathf.Clamp(newPosition.z,-moveDistance,moveDistance);

        transform.position = newPosition;
    }
}
