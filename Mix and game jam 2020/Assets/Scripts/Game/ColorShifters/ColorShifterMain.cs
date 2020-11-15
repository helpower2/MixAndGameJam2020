using System;
using System.Collections.Generic;
using Game.Beat;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;


namespace Game.ColorShifters
{
    public class ColorShifterMain : MonoBehaviour, IBeat
    {
        private readonly List<ColorShift> colorShifts = new List<ColorShift>();

        private void Start()
        {
            colorShifts.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ColorShift>());
        }

        public void Beat(int index)
        {
            foreach (var shift in colorShifts)
            {
                if (shift.switchEver == 0 || shift.stopped)
                    continue; //can not devide 0 or is stopped

                if (index % shift.switchEver != shift.offSet)
                    continue; //not ready yet

                shift.currentColor = shift.colors.
                    Except(new Color[1] {shift.currentColor}) //We dont need the current colour 
                    .ToArray()[Random.Range(0, shift.colors.Length - 2)]; //grab a random color
                
                shift.ChangeColor(shift.currentColor);

            }
        }

        public void HalfBeat(int index) { }
    }

    public abstract class ColorShift : MonoBehaviour
    {
        public bool stopped = false;
        public Color[] colors = new Color[0];
        public int switchEver = 0;
        public int offSet = 0;
        public Color currentColor = Color.black;

        public abstract void ChangeColor(Color color);
    }
}