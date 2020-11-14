using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Beat;
using UnityEngine;
using UnityEngine.UI;


public class ColorSwitcherText : MonoBehaviour, IBeat
{
    public Text text;
    public Color[] colors;
    public BeatType beatType = BeatType.Downbeat;
    public int switchEvery = 0;
    public int offset;
    private Color _currentColor = Color.black;
    
    public void HalfBeat(int index) { }

    public void Beat(int index)//called every beat
    {
        if (switchEvery == 0)
        {
            if (BeatIndex.IsBeat(beatType))// checks if beat is downbeat
            {
                //change color
                ChangeColor();
            }
        }
        else
        {
            if (index % switchEvery == offset)
            {
                ChangeColor();                
            }
        }

    }

    private void ChangeColor()
    {
       _currentColor = colors.Except(new Color[1] {_currentColor}).ToArray()[Random.Range(0, colors.Length - 2)];
        text.color = _currentColor;
    }
}
