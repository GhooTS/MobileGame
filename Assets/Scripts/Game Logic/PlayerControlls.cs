using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour,IPlayerControls
{
    private Rect topButton, bottomButton, fireButton;

    private void Start()
    {

        var halfWidth = Screen.width / 2f;
        var halfHeight = Screen.height / 2f;


        bottomButton = new Rect(0,0, halfWidth, halfHeight);
        topButton = bottomButton;
        topButton.y += halfHeight;

        fireButton = new Rect(halfWidth, 0, halfWidth, halfHeight * 2f);
    }

    public bool GetFireInput()
    {
        var touches = Input.touches;

        foreach (var touch in touches)
        {
            if (touch.phase == TouchPhase.Began && fireButton.Contains(touch.position))
            {
                Debug.Log("Fire Button Press");
                return true;
            }
        }

        return Input.GetKeyDown(KeyCode.Space);
    }

    public float GetVerticalInput()
    {
        var touches = Input.touches;

        foreach (var touch in touches)
        {
            if (touch.phase == TouchPhase.Stationary && topButton.Contains(touch.position))
            {
                return 1f;
            }
            else if (touch.phase == TouchPhase.Stationary && bottomButton.Contains(touch.position))
            {
                return -1f;
            }
        }

        return Input.GetAxisRaw("Vertical");
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(topButton.center, topButton.size);
        Gizmos.DrawWireCube(bottomButton.center, bottomButton.size);
        Gizmos.DrawWireCube(fireButton.center, fireButton.size);
    }
}
