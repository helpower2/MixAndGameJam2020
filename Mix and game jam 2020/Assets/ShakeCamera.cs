using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Beat;
using UnityEngine;


public class ShakeCamera : Beat
{
    public List<int> shakeOn = new List<int>();
    public bool shakeOnStart;
    public bool shakeOnEnable;
    public float intensity = 5f;
    public float time = 1f;

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
        Camera.main.DOShakePosition(intensity, time);
    }
}
