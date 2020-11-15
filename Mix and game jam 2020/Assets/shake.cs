using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DG.Tweening;
using Game.Beat;
using UnityEngine;

public class shake : Beat
{

    public GameObject gameObjectToShake;
    public List<int> shakeOn = new List<int>();
    public bool shakeOnStart;
    public bool shakeOnEnable;
    public float intensity = 5f;
    public float time = 1f;

    private void Awake()
    {
        gameObjectToShake = gameObjectToShake == null ? gameObject : gameObjectToShake;
    }

    private void Start()
    {
        if (shakeOnStart)
        {
            Shake();
        }
    }

    private void OnEnable()
    {
        if (shakeOnEnable)
        {
            Shake();
        }
    }

    public override void OnBeat(int beat)
    {
        if (shakeOn.Contains(beat))
        {
            //shake
            Shake();
        }
    }

    public void Shake()
    {
        gameObjectToShake.transform.DOShakePosition(time, intensity);
    }
}
