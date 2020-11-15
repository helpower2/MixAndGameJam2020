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
        yield return new WaitForSeconds(0.5f);
        StartMusic();
    }

    //[Button]
    public void StartMusic()
    {
        //sets the correct bpm
        BeatIndex.BeatsPerMinute = bpm;

        //sets the audio setting and starts playing
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
        beatMaster.StartBeat();//starts the beat master
    }

    public void StopMusic()
    {
        audioSource.Stop();
        beatMaster.ResetBeat();
        beatMaster.Stopbeat();
    }
    
}
