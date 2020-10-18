using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SceneManager")]
public class ScirptableSceneManager : ScriptableObject
{

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
