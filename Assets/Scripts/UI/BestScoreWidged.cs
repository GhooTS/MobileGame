using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class BestScoreWidged : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI bestScoreText;
    public UnityEvent onNewRecord;

    private void OnEnable()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        bestScoreText.text = scoreManager.BestScore.ToString();
        if (scoreManager.HasNewRecord)
        {
            onNewRecord?.Invoke();
        }
    }
}
