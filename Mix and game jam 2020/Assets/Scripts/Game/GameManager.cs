using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>, IBeat
{
    public PlayerController playerController;
    public AudioSource pickupSound;
    public int resetAfterXBeats = 8;
    public GameObject finishUI;
    public GameObject lostUI;
    public GameObject blurPanel;
    public static bool dead = false;
    public static bool won = false;
    public static int score = 0;

    public readonly List<IReset> resets = new List<IReset>();
    public readonly List<Pickup> pickups = new List<Pickup>();
    private void Awake()
    {
        DestroyOnLoad = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        resets.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IReset>());
        pickups.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<Pickup>());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dead = false;
        won = false;
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

    public void OnPlayerFinish()
    {
        won = true;
        blurPanel.SetActive(true);
        finishUI.SetActive(true);
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
        playerController.SetPosition(playerController.spawnPoint);
        //reset music
        MusicManager.Instance.StartMusic();
        resets.ForEach(x=>x.WorldReset());
        blurPanel.SetActive(false);
        finishUI.SetActive(false);
        lostUI.SetActive(false);
        
        //restart pickups 
        pickups.ForEach(x => x.gameObject.SetActive(true));
    }

    public void Beat(int index)
    {
        if (dead && (index % resetAfterXBeats) == 0)//try to restart and keep the music smooth
        {
            blurPanel.SetActive(true);
            lostUI.SetActive(true);
            MusicManager.Instance.StopMusic();
            MusicManager.Instance.beatMaster.ResetBeat();
        }
    }

    public void HalfBeat(int index) { }
    
    public interface IReset
    {
        void WorldReset();
    }
}
