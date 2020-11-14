#region

using System;
using System.Collections;
using UnityEngine;

#endregion

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object Locked = new object();

    /// <summary>
    ///     When Unity quits, it destroys objects in a random order.
    ///     In principle, a Singleton is only destroyed when application quits.
    ///     If any script calls Instance after it have been destroyed,
    ///     it will create a buggy ghost object that will stay on the Editor scene
    ///     even after stopping playing the Application. Really bad!
    ///     So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    private static bool _applicationIsQuitting;

    /// <summary>
    /// when unity loads a new scene let the Instance be destroyed
    /// </summary>
    protected bool DestroyOnLoad
    {
        get => !_dontDestroyOnLoad;
        set => _dontDestroyOnLoad = !value;
    }

    private bool _dontDestroyOnLoad = false;
    
    
    /// <summary>
    ///     will get or create a Instance of <T>.
    /// </summary>
    public static T Instance
    {
        set
        {
            if (_instance == null || _instance == value)
                _instance = value;

            Destroy(_instance);
            _applicationIsQuitting = false;
            _instance = value;
        }
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning(
                    $"[Singleton] Instance {typeof(T)} '" +
                    "already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (Locked)
            {
                if (_instance == null)
                {
                    var instances = FindObjectsOfType<T>();
                    if (instances.Length > 1)
                    {
                        Debug.LogError(
                            "[Singleton] Something went really wrong" +
                            ", there are too many Singletons; deleting them: ");
                        for (var i = 1; i < instances.Length; i++)
                        {
                            Debug.LogError("Deleting " + instances[i].gameObject.name);
                            Destroy(instances[i].gameObject);
                        }
                    }
                    else if (instances.Length == 0)
                    {
                        var singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "[Singleton] " + typeof(T);

                        DontDestroyOnLoad(singleton);
                        string text = "[Singleton] An instance of {typeof(T)} " +
                                      $"is needed in the scene, so ' {singleton} whas created";
                                      Debug.Log(text);
                    }

                    _instance = FindObjectOfType<T>();
                }

                return _instance;
            }
        }
    }

    private void Start()
    {
        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (_dontDestroyOnLoad)
        {
            _applicationIsQuitting = true; 
        }
    }
}