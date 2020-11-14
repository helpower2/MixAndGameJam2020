using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Beat;
using UnityEngine;


public class ColorSwitcher : MonoBehaviour, IBeat
{
    public SpriteRenderer sprite;
    public Color[] colors;
    public BeatType beatType = BeatType.Downbeat;
    private Color _currentColor = Color.black;
    
    public void HalfBeat(int index) { }

    public void Beat(int index)//called every beat
    {
        if (BeatIndex.IsBeat(beatType))// checks if beat is downbeat
        {
            //change color
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
       _currentColor = colors.Except(new Color[1] {_currentColor}).ToArray()[Random.Range(0, colors.Length - 2)];
        sprite.color = _currentColor;
    }
}
