using System;
using Game.ColorShifters;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ColorSwitcherText : ColorShift
{
    public Text text;

    private void Reset()
    {
        text = GetComponent<Text>();
    }

    public override void ChangeColor(Color color)
    {
        text.color = color;
    }
}
