using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopMusicOnFinishLevel();
    }

    void StopMusicOnFinishLevel()
    {
        if (musicManager != null)
            musicManager.StopMusic();
    }
}
