using UnityEngine;
using UnityEngine.UI;

public class BackgroundSceneLoaderBar : MonoBehaviour
{
    public Image loadingBar;
    public BackgroundSceneLoader sceneLoader;

    private void Update()
    {
        loadingBar.fillAmount = sceneLoader.GetProgress();
    }
}
