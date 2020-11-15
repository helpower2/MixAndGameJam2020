using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public int pickupScore;
    public UnityEvent onPickup = new UnityEvent();
}
