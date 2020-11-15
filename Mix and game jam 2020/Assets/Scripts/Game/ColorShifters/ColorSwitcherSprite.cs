using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Beat;
using Game.ColorShifters;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorSwitcherSprite : ColorShift
{
    public SpriteRenderer spriteRenderer;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
