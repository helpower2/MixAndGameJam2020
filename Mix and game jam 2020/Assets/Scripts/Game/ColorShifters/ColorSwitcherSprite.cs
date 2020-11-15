using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Beat;
using Game.ColorShifters;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorSwitcherSprite : ColorShift
{
    public SpriteRenderer spriteRenderer;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeColor(colors[Random.Range(0, colors.Length -1)]);
    }

    public override void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
