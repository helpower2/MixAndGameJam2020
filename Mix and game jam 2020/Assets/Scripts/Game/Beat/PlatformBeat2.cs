using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformBeat2 : Beat
{
    public string voidTag = "Void";
    public string platformTag = "Platform";
    private SpriteRenderer spriteRenderer;
    private bool active;
    private int moveIndex;
    public bool[] platformBeat;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void OnBeat(int beat)
    {
        moveIndex++;
        active = platformBeat[moveIndex % platformBeat.Length];
        spriteRenderer.enabled = active;
        tag = active ? platformTag : voidTag;
        //Debug.Log(beat);
    }
}
