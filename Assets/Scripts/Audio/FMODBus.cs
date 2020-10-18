using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Threading.Tasks;

[CreateAssetMenu(menuName = "FMOD Audio/Bus")]
public class FMODBus : ScriptableObject
{
    public string busPath;

    private string volumeKey;
    private string muteKey;
    private bool mute;
    private float volume;

    private bool prefsLoaded = false;


    public void OnEnable()
    {
        volumeKey = $"{busPath}_Volume";
        muteKey = $"{busPath}_Mute";

        prefsLoaded = false;
    }

    public void LoadPrefs()
    {
        if (prefsLoaded == false)
        {
            //Load volume options
            if (PlayerPrefs.HasKey(volumeKey)) SetVolume(PlayerPrefs.GetFloat(volumeKey));
            else volume = .5f;

            //Load mute options
            if (PlayerPrefs.HasKey(muteKey)) SetMute(PlayerPrefs.GetInt(muteKey) == 1);
            else mute = false;
        }

        prefsLoaded = true;
    }

    public float GetVolume()
    {
        LoadPrefs();
        return volume;
    }


    public bool GetMute()
    {
        LoadPrefs();
        return mute;
    }

    public void SetVolume(float volume)
    {
        RuntimeManager.GetBus(busPath).setVolume(volume);
        PlayerPrefs.SetFloat(volumeKey, volume);
        this.volume = volume;
    }

    public void SetMute(bool mute)
    {
        RuntimeManager.GetBus(busPath).setMute(mute);
        PlayerPrefs.SetInt(muteKey, mute ? 1 : 0);
        this.mute = mute;
    }
}
