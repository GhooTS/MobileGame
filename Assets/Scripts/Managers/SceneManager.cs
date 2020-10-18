using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SceneManager")]
public class SceneManager : ScriptableObject
{

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
