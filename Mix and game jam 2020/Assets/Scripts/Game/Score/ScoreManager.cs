using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private int score = 0;
    
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreChanged.Invoke();
        }
    }
    public UnityEvent scoreChanged = new UnityEvent();
    
}
