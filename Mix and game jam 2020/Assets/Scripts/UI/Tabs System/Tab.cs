using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("TabMenu/Tab")]
public class Tab : TabButton
{
    [Space(3)]
    [Header("Tab text")]
    public Text text;
    
    [Header("Colors")]
    public Color[] activeColors = new Color[2];
    public Color[] inactiveColors = new Color[2];
    
    
    private new void Awake()
    {
        base.Awake();
        onTabSelected.AddListener(TabSelected);
        onTabDeselected.AddListener(TabDeselected);
    }

    private void TabSelected()
    {
        if (background != null)
            background.color = activeColors[0];
        if (text != null)
            text.color = activeColors[1];
    }

    private void TabDeselected()
    {
        if (background != null)
            background.color = inactiveColors[0];
        if (text != null)
            text.color = inactiveColors[1];
    }
}
