using System;
using System.Collections;
using System.Collections.Generic;
using Game.ColorShifters;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ColorShiftCamera : ColorShift
{
    public Camera cam;

    private void Reset()
    {
        cam = GetComponent<Camera>();
    }

    public override void ChangeColor(Color color)
    {
        cam.backgroundColor = color;
    }
    
}
