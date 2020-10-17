using TMPro;
using UnityEngine;

public class ScoreMultiplayerWidged : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI multiplayerText;

    private void Start()
    {
        multiplayerText.text = "";
    }

    private void OnEnable()
    {
        scoreManager.onScoreMultiplayerChanaged.AddListener(UpdateMultiplayer);
    }

    private void OnDisable()
    {
        scoreManager.onScoreMultiplayerChanaged.RemoveListener(UpdateMultiplayer);
    }


    public void UpdateMultiplayer()
    {
        if (scoreManager.CurrentScoreMultiplayer != 1f)
        {
            multiplayerText.text = $"X{scoreManager.CurrentScoreMultiplayer}";
        }
        else
        {
            multiplayerText.text = "";
        }
    }

}
