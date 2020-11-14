#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class ThreadManager : MonoBehaviour
{
    private static readonly List<Action> executeOnMainThread = new List<Action>();
    private static readonly List<Action> ExecuteCopiedOnMainThread = new List<Action>();
    private static bool _actionToExecuteOnMainThread;

    private void FixedUpdate()
    {
        UpdateMain();
    }

    /// <summary>Sets an action to be executed on the main thread.</summary>
    /// <param name="_action">The action to be executed on the main thread.</param>
    public static void ExecuteOnMainThread(Action _action)
    {
        if (_action == null)
        {
            Console.WriteLine("No action to execute on main thread!");
            return;
        }

        lock (executeOnMainThread)
        {
            executeOnMainThread.Add(_action);
            _actionToExecuteOnMainThread = true;
        }
    }

    /// <summary>Executes all code meant to run on the main thread. NOTE: Call this ONLY from the main thread.</summary>
    public static void UpdateMain()
    {
        if (_actionToExecuteOnMainThread)
        {
            ExecuteCopiedOnMainThread.Clear();
            lock (executeOnMainThread)
            {
                ExecuteCopiedOnMainThread.AddRange(executeOnMainThread);
                executeOnMainThread.Clear();
                _actionToExecuteOnMainThread = false;
            }

            foreach (var action in ExecuteCopiedOnMainThread) action();
            
            //for (var i = 0; i < executeCopiedOnMainThread.Count; i++) executeCopiedOnMainThread[i]();
        }
    }
}