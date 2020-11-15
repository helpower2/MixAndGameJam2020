using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volumesettings : MonoBehaviour
{

    public AudioMixer mainMixer;

    public Text valueText;

    public void SetMainVolume(float volume)
    {
        mainMixer.SetFloat(VolumeTypes.Master.ToString(), volume);
        valueText.text = Mathf.RoundToInt(volume).ToString() + " dB";
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat(VolumeTypes.Music.ToString(), volume);
        valueText.text = Mathf.RoundToInt(volume).ToString() + " dB";
    }

    public void SetPickupVolume(float volume)
    {
        mainMixer.SetFloat(VolumeTypes.Pickups.ToString(), volume);
        valueText.text = Mathf.RoundToInt(volume).ToString() + " dB";
    }
    public void SetButtonVolume(float volume)
    {
        mainMixer.SetFloat(VolumeTypes.Buttons.ToString(), volume);
        valueText.text = Mathf.RoundToInt(volume).ToString() + " dB";
    }


    public enum VolumeTypes
    {
        Master,
        Music,
        Pickups,
        Buttons,
    }
}
