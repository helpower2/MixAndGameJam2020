using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float movementY = 0.4f;
    public float time = 2f;

    private void Start()
    {
        transform.DOMoveY(movementY + transform.position.y, time).SetLoops(-1, LoopType.Yoyo);
    }
}
