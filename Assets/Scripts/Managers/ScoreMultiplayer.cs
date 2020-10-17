using UnityEngine;

[System.Serializable]
public struct ScoreMultiplayer
{
    [Min(0)]
    public int comboRequired;
    [Min(1f)]
    public float multiplayer;
}
