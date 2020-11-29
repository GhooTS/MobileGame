using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlls : MonoBehaviour,IPlayerControls
{

    public bool GetFireInput()
    {
        float halfScreenSize = Screen.width / 2f;

		for(int i = 0;i < Input.touchCount;i++)
		{
            //continue if current touch is over UI
            if (IsOverUI(i)) continue;
			
			var touch = Input.GetTouch(i);
			
            if (touch.phase == TouchPhase.Began && touch.position.x > halfScreenSize)
			{
				return true;
			}
		}

        #if UNITY_EDITOR 
        return Input.GetKeyDown(KeyCode.Space);
        #else
        return false;
		#endif
    }

    public float GetVerticalInput()
    {

        float halfScreenWidth = Screen.width / 2f;
        float halfScreenHeight = Screen.height / 2f;

		for(int i = 0;i < Input.touchCount;i++)
		{
            //continue if current touch is over UI
			if(IsOverUI(i)) continue;

            var touch = Input.GetTouch(i);

            if (touch.position.x < halfScreenWidth)
            {
                return touch.position.y > halfScreenHeight ? 1f : -1f;
            }
		}

        #if UNITY_EDITOR 
        return Input.GetAxisRaw("Vertical");
        #else
        return 0;
        #endif
    }



    private bool IsOverUI(int toucheIndex)
    {
        var eventSystem = EventSystem.current;

        if(eventSystem != null)
        {
            if (eventSystem.IsPointerOverGameObject(toucheIndex))
            {
                return true;
            }
        }

        return false;
    }
}
