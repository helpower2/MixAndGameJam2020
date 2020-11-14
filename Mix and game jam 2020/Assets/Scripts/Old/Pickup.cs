using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    public int pickupScore;
    public UnityEvent onPickup = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        //it is the player
        ScoreManager.Instance.Score += pickupScore;
        onPickup.Invoke();
        Destroy(this.gameObject);
    }
}
