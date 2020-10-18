using UnityEngine;
using UnityEngine.UI;

public class FMODBusController : MonoBehaviour
{
    public FMODBus bus;
    public Slider volumeSlider;
    public Toggle muteToggle;


    private void Start()
    {
        volumeSlider.value = bus.GetVolume();
        volumeSlider.onValueChanged.AddListener(bus.SetVolume);
        muteToggle.isOn = bus.GetMute();
        muteToggle.onValueChanged.AddListener(bus.SetMute);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(bus.SetVolume);
        muteToggle.onValueChanged.RemoveListener(bus.SetMute);
    }
}
