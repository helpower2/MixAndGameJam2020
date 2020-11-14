
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scenes.Indexes;
using UnityEngine.Events;

namespace Scenes
{
    public class LoadingProgressEvent : UnityEvent<int>{}
    
    
    public class LoadingSceneManager : Singleton<LoadingSceneManager>
    {
        public GameObject loadingScreen;
        public ThreadPriority loadingSpeed = ThreadPriority.BelowNormal;

        public LoadingProgressEvent loadingProgress = new LoadingProgressEvent();
        //note the total progress can go backwards because of ILoading tasks being added during loading
        private float _totalProgress;
        private bool _isLoading;
        private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
        private List<ILoading> _loadingThings = new List<ILoading>();

        private Coroutine _getTotalProgressCount;
        private Coroutine _getLoadProgress;

        /// <summary>
        /// will unload A scene Async 
        /// </summary>
        /// <param name="sceneToUnload">the scene to unload</param>
        /// <param name="mode">the unloading options</param>
        public IEnumerator UnloadSceneAsync(SceneIndexes sceneToUnload, UnloadSceneOptions mode = UnloadSceneOptions.None)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(Loading(true));
            _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)sceneToUnload, mode));
            StartProgressCounters();
        }

        /// <summary>
        /// will load a scene async
        /// </summary>
        /// <param name="sceneToLoad">the scene to load</param>
        /// <param name="mode">the loading options</param>
        public IEnumerator LoadSceneAsync(SceneIndexes sceneToLoad, LoadSceneMode mode)
        {
            yield return new WaitForEndOfFrame();
            Application.backgroundLoadingPriority = loadingSpeed;
            //Debug.Log($"started loading {sceneToLoad.ToString()}");
            StartCoroutine(Loading(true));
            var sceneLoadDing = SceneManager.LoadSceneAsync((int) sceneToLoad, mode);
            _scenesLoading.Add(sceneLoadDing);
            StartProgressCounters();
        }
        
        /// <summary>
        /// for adding a loading proses. aka if a scene has loaded and you have to spawn things
        /// </summary>
        /// <param name="proses">the proses</param>
        /// <param name="showLoadingScreen">show the loading screen</param>
        public void AddLoadingProses(ILoading proses, bool showLoadingScreen = true)
        {
            if (showLoadingScreen) 
                StartCoroutine(Loading(true));
            _loadingThings.Add(proses);
            StartProgressCounters();
        }

        private IEnumerator GetLoadProgress()
        {
            
            //Debug.Log("started loading");
            foreach (var scene in _scenesLoading.ToArray())
            {
                yield return new WaitUntil(()=>scene?.progress >0.8);
                //Debug.Log($"Activating scene");
                yield return new WaitUntil(()=> scene.isDone);
                
            }
            //Debug.Log("Done loading scenes");

            foreach (var loadingThing in _loadingThings.ToArray())
            {
                yield return new WaitUntil(() => loadingThing.IsDone);
            }
            //Debug.Log("done loading things");
            //done loading
            StartCoroutine(Loading(false));
        }

        private IEnumerator GetTotalProgressCount()
        {
            //Debug.Log("start calculating loading progress");
            do
            {
                float scenesProgress = 0;
                float restOfLoadingThings = 0;

                _loadingThings.ForEach(x => restOfLoadingThings += x?.Progress ?? 1);
                _scenesLoading.ForEach(x => scenesProgress += x?.progress ?? 1);

                scenesProgress = (scenesProgress / _scenesLoading.Count) * 100f / _scenesLoading.Count;
                restOfLoadingThings = (restOfLoadingThings / _loadingThings.Count) * 100f / _loadingThings.Count;

                scenesProgress = (float.IsNaN(scenesProgress) ? 0 : scenesProgress);
                _totalProgress = (float.IsNaN(restOfLoadingThings) ? 0 : restOfLoadingThings);
                _totalProgress = scenesProgress + _totalProgress;
                
                //Debug.Log($"Loading {_totalProgress.ToString()}");
                yield return new WaitForEndOfFrame();
            } while (_isLoading);
        }
        

        private IEnumerator Loading(bool active)
        {
            _isLoading = active;
            if (!active)
            {
                yield return new WaitForEndOfFrame();
                if (_getLoadProgress != null) StopCoroutine(_getLoadProgress);
                if (_getTotalProgressCount != null) StopCoroutine(_getTotalProgressCount);
                //_scenesLoading = new List<AsyncOperation>();
                //_loadingThings = new List<ILoading>();
                Debug.Log(active + " Active");
            }

            if (loadingScreen != null)
            {
                loadingScreen.SetActive(active); 
            }
            
        }

        private void StartProgressCounters()
        {
            //Debug.Log("starting coroutines");
            
            if (_getLoadProgress != null) StopCoroutine(_getLoadProgress);
            if (_getTotalProgressCount != null) StopCoroutine(_getTotalProgressCount);
            
            _getTotalProgressCount = StartCoroutine(GetTotalProgressCount());
            _getLoadProgress = StartCoroutine(GetLoadProgress());
        }

    }
    public interface ILoading
    {
        bool IsDone { get; }
        float Progress { get; }
        string ProsesName { get; }
    }
}

