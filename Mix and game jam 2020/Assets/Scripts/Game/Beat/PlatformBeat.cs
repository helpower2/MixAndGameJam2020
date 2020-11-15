using System;
using System.Collections;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformBeat : MonoBehaviour, IBeat
{
    public int beatMultiplier = 1;
    public int offset = 0;
    public BeatType beatType;
    public bool useBeatType;
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
        var active = false;
        if (useBeatType)
        {
            active = BeatIndex.IsBeat(beatType, offset);
        }
        else
        {
            if (platformBeat.Length == 0) 
                return;
            active = (platformBeat[((index + offset) / beatMultiplier) % platformBeat.Length]);
        }
        spriteRenderer.enabled = active;
        tag = active ? platformTag : voidTag;

    }

    public void HalfBeat(int index) { }

#if UNITY_EDITOR
    private void OnGUI()
    {
        beatType = (Game.Beat.BeatType) EditorGUILayout.EnumFlagsField(beatType);
    }
#endif

}
