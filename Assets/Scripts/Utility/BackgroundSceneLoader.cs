using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BackgroundSceneLoader : MonoBehaviour
{
    public Image loadingBar;
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

        loadingBar.fillAmount = sceneLoadingOperation.progress;
    }
}
