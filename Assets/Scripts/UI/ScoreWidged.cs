using TMPro;
using UnityEngine;
public class ScoreWidged : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI scoreText;


    private void OnEnable()
    {
        scoreManager.onScoreChanged.AddListener(UpdateScore);
        UpdateScore();
    }

    private void OnDisable()
    {
        scoreManager.onScoreChanged.RemoveListener(UpdateScore);
    }

    private void UpdateScore()
    {
        scoreText.text = scoreManager.CurrentScore.ToString();
    }
}
