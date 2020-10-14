using TMPro;
using UnityEngine;
public class ScoreWidged : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "0";
    }

    private void OnEnable()
    {
        scoreManager.onScoreChanged.AddListener(UpdateScore);
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
