using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public UnityEvent onPickup = new UnityEvent();
    public float movementY = 0.4f;
    public float time = 2f;

    public void Start()
    { 
        transform.DOMoveY(movementY + transform.position.y, time).SetLoops(-1, LoopType.Yoyo);
    }
}
