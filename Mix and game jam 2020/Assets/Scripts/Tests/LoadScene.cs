using System.Collections;
using System.Collections.Generic;
using Scenes;
using Scenes.Indexes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(LoadingSceneManager.Instance.LoadSceneAsync(SceneIndexes.MainMenu, LoadSceneMode.Additive));
    }
    
}
