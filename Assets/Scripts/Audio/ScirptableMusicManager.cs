using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "FMOD Audio/Music Manager")]
public class ScirptableMusicManager : ScriptableObject
{
    public bool createManagerIfMissing;
    public MusicManager managerPrefab;
    [SerializeField]
    private FMODParameter inMainMenuParam;
    [SerializeField]
    private FMODParameter battleMusicID;


    public void Start()
    {
        TryCreateManager();
        MusicManager.Current.Play();
    }

    public void Stop()
    {
        TryCreateManager();
        MusicManager.Current.Stop();
    }

    public void SetParameter(string name,float value,bool ignoreseekspeed = false,bool isGlobal = false)
    {
        TryCreateManager();
        if (isGlobal) RuntimeManager.StudioSystem.setParameterByName(name, value, ignoreseekspeed);
        else MusicManager.Current.SetParameter(name, value, ignoreseekspeed);
    }

    public void SetParameter(FMOD.Studio.PARAMETER_ID id, float value, bool ignoreseekspeed = false,bool isGlobal = false)
    {
        TryCreateManager();
        if (isGlobal) RuntimeManager.StudioSystem.setParameterByID(id, value, ignoreseekspeed);
        else MusicManager.Current.SetParameter(id, value, ignoreseekspeed);
    }


    public void SetInMainMenu(bool inMainMenu)
    {
        SetParameter(inMainMenuParam.name, inMainMenu ? 1f : 0f,false,inMainMenuParam.isGlobal);
    }

    public void SetBattleMusicID(int index)
    {
        SetParameter(battleMusicID.name, index,false,battleMusicID.isGlobal);
    }

    private void TryCreateManager()
    {
        if(createManagerIfMissing && MusicManager.Current == null)
        {
            Instantiate(managerPrefab);
        }
    }
}
