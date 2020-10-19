using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BackgroundSceneLoader : MonoBehaviour
{
    public int sceneIndex;

    public UnityEvent onSceneLoaded;

    private int thisSceneIndex;
    private AsyncOperation sceneLoadingOperation;

    private void Start()
    {
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneIndex,LoadSceneMode.Additive);
    }


    private void Update()
    {
        if (sceneLoadingOperation.isDone)
        {
            onSceneLoaded?.Invoke();
            SceneManager.UnloadSceneAsync(thisSceneIndex);
        }

       
    }

    public float GetProgress()
    {
        return sceneLoadingOperation != null ? sceneLoadingOperation.progress : 0f;
    }
}
