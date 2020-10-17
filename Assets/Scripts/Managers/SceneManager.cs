﻿using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SceneManager")]
public class SceneManager : ScriptableObject
{

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
