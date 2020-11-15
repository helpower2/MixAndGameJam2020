using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Beat;
using UnityEngine;


public class ClockBeat : Beat
{
    public List<GameObject> objects = new List<GameObject>();
    public BeatType beatTypeDisable;
    public BeatType beatTypeEnable;
    private bool active;
    private int moveIndex;

    public override void OnBeat(int beat)
    {
        if (BeatIndex.IsBeat(beatTypeDisable))
            DisableAllWiser();

        if (BeatIndex.IsBeat(beatTypeEnable))
        {
            DisableWiser(moveIndex);
            moveIndex++;
            SetWiser(moveIndex);
        }
    }

    public void DisableAllWiser()
    {
        objects.ForEach(x => x.SetActive(false));
    }

    public void DisableWiser(int index)
    {
        objects[index % objects.Count].SetActive(false);
    }
    
    public void SetWiser(int index)
    {
        objects[index % objects.Count].SetActive(true);
    } 
}
