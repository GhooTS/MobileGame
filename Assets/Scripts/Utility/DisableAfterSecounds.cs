using UnityEngine;

public class DisableAfterSecounds : MonoBehaviour
{
    public float duration;
    private float disableTime;

    private void OnEnable()
    {
        disableTime = Time.time + duration;
    }

    private void Update()
    {
        if (disableTime < Time.time) gameObject.SetActive(false);
    }
}
