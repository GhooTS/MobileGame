using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool playOnAwake;
    public static MusicManager Current { get; private set; }
    [EventRef]
    [SerializeField]
    private string musicEventPath = default;
    private StudioEventEmitter emitter;


    private void Awake()
    {
        if(Current == null)
        {
            Current = this;
            DontDestroyOnLoad(gameObject);
            if(TryGetComponent(out emitter) == false)
            {
                emitter = gameObject.AddComponent<StudioEventEmitter>();
                emitter.Event = musicEventPath;
            }

            if (playOnAwake) Play();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    public void Play()
    {
        emitter.Play();
    }

    public bool IsPlaying()
    {
        return emitter.IsPlaying();
    }

    public void SetParameter(string path,float value,bool ignoreseekspeed = false)
    {
        emitter.SetParameter(path, value, ignoreseekspeed);
    }

    public void SetParameter(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreseekspeed = false)
    {
        emitter.SetParameter(id, value, ignoreseekspeed);
    }

    public void Stop()
    {
        emitter.Stop();
    }

}
