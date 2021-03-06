﻿using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Score/Score Manager")]
public class ScoreManager : ScriptableObject
{
    [SerializeField]
    private ScoreMultiplayer[] scoreMultiplayer;
    [SerializeField]
    private float comboResetDuration;

    public UnityEvent onScoreChanged;

    public UnityEvent onComboChanged;
    public UnityEvent onComboRestarted;
    public UnityEvent onScoreMultiplayerChanaged;

    public float CurrentScore { get; private set; }
    public int CurrentCombo { get; private set; }
    public float ComboResetDuration { get { return comboResetDuration; } }
    public float CurrentScoreMultiplayer { get; private set; }
    public float BestScore { get; private set; }
    public bool HasNewRecord { get; private set; }


    private float lastScoreUpdateTime;
    private int multiplayerIndex;

    private const string bestScoreKey = "PLAYER_BESTSCORE"; 

    private void OnEnable()
    {
        BestScore = PlayerPrefs.GetFloat(bestScoreKey); 
    }

    public void ResetAll()
    {
        ResetScore();
        ResetCombo();
    }

    public void ResetScore()
    {
        CurrentScore = 0f;
        lastScoreUpdateTime = 0f;
        onScoreChanged?.Invoke();
    }

    public void ResetCombo()
    {
        CurrentCombo = 0;
        multiplayerIndex = -1;
        CurrentScoreMultiplayer = GetScoreMultiplayer();
        onScoreMultiplayerChanaged?.Invoke();
        onComboRestarted?.Invoke();
    }

    public void AddScore(float score)
    {
        UpdateScoreMultiplayer();

        CurrentScore += score * CurrentScoreMultiplayer;

        onScoreChanged?.Invoke();
    }

    public void UpdateScoreMultiplayer()
    {
        if (Time.time + Time.deltaTime - lastScoreUpdateTime < comboResetDuration || CurrentCombo == 0)
        {
            CurrentCombo++;
            onComboChanged?.Invoke();
            int nextMultiplayer = multiplayerIndex + 1;
            if (nextMultiplayer < scoreMultiplayer.Length && scoreMultiplayer[nextMultiplayer].comboRequired <= CurrentCombo)
            {
                multiplayerIndex = nextMultiplayer;
                CurrentScoreMultiplayer = GetScoreMultiplayer();
                onScoreMultiplayerChanaged?.Invoke();
            } 
        }
        else
        {
            CurrentCombo = 0;
            onComboChanged?.Invoke();
            if (multiplayerIndex != -1)
            {
                multiplayerIndex = -1;
                onScoreMultiplayerChanaged?.Invoke();
            }
        }

        lastScoreUpdateTime = Time.time;
    }

    private float GetScoreMultiplayer()
    {
        return multiplayerIndex != -1 && multiplayerIndex < scoreMultiplayer.Length ? scoreMultiplayer[multiplayerIndex].multiplayer : 1f;
    }


    public void UpdateBestScore()
    {
        HasNewRecord = CurrentScore > BestScore;
        if(HasNewRecord)
        {
            BestScore = CurrentScore;
            PlayerPrefs.SetFloat(bestScoreKey, BestScore);
        }
    }

}
