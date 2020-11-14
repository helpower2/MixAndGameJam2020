using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using Game.Beat;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    public int bpm;
    public AudioClip music;
    [HideInInspector] public AudioSource audioSource;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [Button]
    public void StartMusic()
    {
        //sets the correct bpm
        BeatIndex.BeatsPerMinute = bpm;
        BeatMaster.Instance.bpm = bpm;
        
        //sets the audio setting and starts playing
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();

        if (BeatMaster.Instance.running)
            return;
        BeatMaster.Instance.StartStopBeat();//starts the beat master
    }
}
