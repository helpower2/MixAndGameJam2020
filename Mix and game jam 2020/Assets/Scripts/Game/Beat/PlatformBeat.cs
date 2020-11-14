using System;
using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformBeat : MonoBehaviour, IBeat
{
    public int beatMultiplier = 1;
    public int offset = 0;
    public bool[] platformBeat;
    public string voidTag = "Void";
    public string platformTag = "Platform";

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Beat(int index)
    {
        if (platformBeat.Length == 0) 
            return;
        
        var active = (platformBeat[((index + offset) / beatMultiplier) % platformBeat.Length]);
        spriteRenderer.enabled = active;
        tag = active ? platformTag : voidTag;

    }

    public void HalfBeat(int index) { }
}
