using System.Collections;
using System.Collections.Generic;
using Scenes;
using Scenes.Indexes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public bool autoLoad = false;
    public float loadSceneAfter = 0f;
    public SceneIndexes sceneToLoad = SceneIndexes.MainMenu;
    public LoadSceneMode loadSceneMode = LoadSceneMode.Additive;
    public bool unloadScene = false;
    public SceneIndexes sceneToUnload = SceneIndexes.GameLevel1;
    
    // Start is called before the first frame update
    void Start()
    {
        if (autoLoad)
        {
            Load();
        }
        
    }

    public void Load()
    {
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(loadSceneAfter);
        StartCoroutine(LoadingSceneManager.Instance.LoadSceneAsync(sceneToLoad, loadSceneMode));
        StartCoroutine(LoadingSceneManager.Instance.UnloadSceneAsync(sceneToLoad, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects));
    }
    
}
