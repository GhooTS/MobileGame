using TMPro;
using UnityEngine;

public class ComboWidged : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI comboText;

    private void Start()
    {
        comboText.text = "";
    }

    private void OnEnable()
    {
        scoreManager.onComboChanged.AddListener(UpdateCombo);
        scoreManager.onComboRestarted.AddListener(UpdateCombo);
    }

    private void OnDisable()
    {
        scoreManager.onComboChanged.RemoveListener(UpdateCombo);
        scoreManager.onComboRestarted.RemoveListener(UpdateCombo);
    }


    public void UpdateCombo()
    {
        if (scoreManager.CurrentCombo != 0)
        {
            comboText.text = scoreManager.CurrentCombo.ToString();
        }
        else
        {
            comboText.text = "";
        }
    }
}
