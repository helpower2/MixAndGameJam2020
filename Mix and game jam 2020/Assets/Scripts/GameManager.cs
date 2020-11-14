using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>, IBeat
{
    public Vector2 playerSpawn;
    public PlayerController playerController;
    public AudioSource pickupSound;
    public int resetAfterXBeats = 8;
    public static bool dead =false;
    public static int score = 0;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        score = 0;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnPickupCollected()
    {
        score++; //increase score
        
        //play sounds
        pickupSound.Play();
    }

    public void OnHitDeadly()
    {
        dead = true;
    }
    
    public void ReloadScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void RestartLevel()
    {
        dead = false;
        score = 0;
        //reset player
        playerController.SetPosition(playerSpawn);
        //reset music
        MusicManager.Instance.StartMusic();
    }

    public void Beat(int index)
    {
        if (dead && (index % resetAfterXBeats) == 0)//try to restart and keep the music smooth
        {
            RestartLevel();
        }
    }

    public void HalfBeat(int index) { }
}
