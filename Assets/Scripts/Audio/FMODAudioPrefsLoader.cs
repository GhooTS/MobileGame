using FMODUnity;
using UnityEngine;

public class FMODAudioPrefsLoader : MonoBehaviour
{
    public FMODBus[] buses;


    private void Start()
    {
        LoadPrefsAndDestroy();
    }

    private void Update()
    {
        LoadPrefsAndDestroy();
    }

    private void LoadPrefsAndDestroy()
    {
        if (RuntimeManager.IsInitialized)
        {
            foreach (var bus in buses)
            {
                bus.LoadPrefs();
            }
            Destroy(this);
        }
    }
}
