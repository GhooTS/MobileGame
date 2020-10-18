using UnityEngine;
using FMODUnity;

public class FMODOneShotAudioPlayer : MonoBehaviour
{
    public void PlayOneShot(string eventPath)
    {
        RuntimeManager.PlayOneShot(eventPath,transform.position);
    }

    public void PlayOneShotAndAttached(string eventPath)
    {
        RuntimeManager.PlayOneShotAttached(eventPath,gameObject);
    }
}
