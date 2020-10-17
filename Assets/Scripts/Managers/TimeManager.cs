using UnityEngine;

[CreateAssetMenu(menuName = "Time/Time Manager")]
public class TimeManager : ScriptableObject
{
    public float defaultTimeScale = 1f;

    public void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void SetDefualtTimeScale()
    {
        Time.timeScale = defaultTimeScale;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }
}
