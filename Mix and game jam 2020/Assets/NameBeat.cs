using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Beat;
using UnityEngine;

public class NameBeat : Beat
{
    public float scale = 0.1f;
    public float duration = 0.2f;
    public LoopType loopType = LoopType.Yoyo;
    public override void OnBeat(int beat)
    {
        //transform.DOScale(transform.localScale + (Vector3.one * scale), duration).SetLoops(1, loopType);
        transform.DOShakeScale(duration, scale).SetLoops(1);
    }
}
