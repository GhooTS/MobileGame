using UnityEngine;
using UnityEngine.UI;

public class ComboTimeBarWidged : MonoBehaviour
{
    public Image bar;
    public ScoreManager scoreManager;

    private float timeLeft;

    private void OnEnable()
    {
        scoreManager.onComboChanged.AddListener(UpdateTimeLeft);
        scoreManager.onComboRestarted.AddListener(HideBar);
    }

    private void OnDisable()
    {
        scoreManager.onComboChanged.RemoveListener(UpdateTimeLeft);
        scoreManager.onComboRestarted.RemoveListener(HideBar);
    }

    private void Update()
    {
        if (bar.enabled)
        {
            timeLeft -= Time.deltaTime;
            bar.fillAmount = timeLeft / scoreManager.ComboResetDuration;
            if(bar.fillAmount == 0)
            {
                scoreManager.ResetCombo();
                HideBar();
            }
        }
    }

    public void UpdateTimeLeft()
    {
        if(bar.enabled == false) bar.enabled = true;
        timeLeft = scoreManager.ComboResetDuration;
    }

    public void HideBar()
    {
        bar.enabled = false;
    }
}
