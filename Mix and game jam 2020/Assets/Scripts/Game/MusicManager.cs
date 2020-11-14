using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EasyButtons;
using Game.Beat;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public int bpm;
    public AudioClip music;
    public BeatMaster beatMaster;
    public static MusicManager Instance;
    [HideInInspector] public AudioSource audioSource;
    
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
        
        beatMaster = beatMaster ?? FindObjectOfType<BeatMaster>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(StartMusicAfterWait());
    }

    private IEnumerator StartMusicAfterWait()
    {
        yield return new WaitForSeconds(1.0f);
        StartMusic();
    }

    //[Button]
    public void StartMusic()
    {
        //sets the correct bpm
        BeatIndex.BeatsPerMinute = bpm;
        beatMaster.bpm = bpm;
        
        //sets the audio setting and starts playing
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();

        if (beatMaster.running)
            return;
        beatMaster.StartStopBeat();//starts the beat master
    }
    
}
