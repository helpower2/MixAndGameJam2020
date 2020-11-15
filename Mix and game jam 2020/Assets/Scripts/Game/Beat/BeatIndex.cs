using System;

namespace Game.Beat
{
    /// <summary>
    /// will keep track of beat info
    /// </summary>
    public static class BeatIndex
    {
        public static int BeatsPerMinute = 60;
        
        public static float BeatsPerSeconds => BeatsPerMinute / 60f;
        public static float SecondsPerBeat => 60f / BeatsPerMinute;
        public static float SecondsPerHalfBeat => 30f / BeatsPerMinute;
        
        //stores the results of CalculateBeat so it does not have to be calculated for every object
        private  static readonly bool[] BeatTypes = new bool[9];
        
        private static int _beat; //local beat index

        /// <summary>
        /// will check if the beat is equal to the beatType
        /// </summary>
        /// <param name="beatType"></param>
        /// <returns>true if equal to the beatType</returns>
        public static bool IsBeat(BeatType beatType ,int offset = 0)
        {
            var beat = _beat;
            var checkTypeValues = Enum.GetValues(typeof(BeatType));
            bool output = false;
            foreach (BeatType value in checkTypeValues)
            {
                if ((beatType & value) == value)
                {
                    switch (value)
                    {
                        case BeatType.Downbeat:
                            output = beat % 4 == 0 + offset; // the first beat 1 / 4 
                            break;
                        case BeatType.Second:
                            output = beat % 4 == 1 + offset; //the second beat 2 / 4 
                            break;
                        case BeatType.OnBeat:
                            output = beat % 4 == 2 + offset; // third beat 3 / 4
                            break;
                        case BeatType.Fourth:
                            output = beat % 4 == 3 + offset; // the fourth beat 4 / 4 
                            break;
                        case BeatType.EighthBeat:
                            output = BeatTypes[4];
                            break;
                        case BeatType.Offbeat:
                            output = BeatTypes[5];
                            break;
                        case BeatType.Backbeat:
                            output = BeatTypes[6];
                            break;
                        case BeatType.HyperBeat:
                            output = beat % 8 == 7 + offset;
                            break;
                        case BeatType.FirstAndThird:
                            output = beat % 2 == 0 + offset;
                            break;
                        default:
                            output = false;
                            break;
                    }

                    if (output)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// will Calculate the beatType of the current beat
        /// </summary>
        /// <param name="beat">the beat index</param>
        public static void CalculateBeat(int beat)
        {
            for (int i = 0; i < BeatTypes.Length; i++)
                BeatTypes[i] = false;
            beat++;
            _beat = beat;

            if (beat % 4 == 0) // down beat
                BeatTypes[0] = true;
            
            if (beat % 4 == 1) // second beat
                BeatTypes[1] = true;
            
            if (beat % 4 == 2) // OnBeat
                BeatTypes[2] = true;
            
            if (beat % 4 == 3) // fourth beat
                BeatTypes[3] = true;

            if (beat % 8 == 7) // EighthBeat
                BeatTypes[4] = true;
            
            if (beat % 8 == 7) // HyperBeat
                BeatTypes[7] = true;
            if (beat % 2 == 0)
                BeatTypes[8] = true;
        }

        /// <summary>
        /// Will Calculate the half Beat Types 
        /// </summary>
        public static void CalculateHalfBeat()
        {
            BeatTypes[5] = true;
            BeatTypes[6] = true;
        }
    }

    /// <summary>
    /// the types of beat we have
    /// </summary>
    /// <remarks>check if correct I might have made a mistake</remarks>
    [Flags]
    public enum BeatType
    {
        Downbeat = 1, //the first beat 1 / 4
        Second = 2, //the second beat 2 / 4
        OnBeat = 4, //the third beat 3 / 4 
        Fourth = 8, //the fourth beat 4 / 4 
        EighthBeat = 16, //the eighth beat 8 / 8
        Offbeat = 32, //ever 1.5 / 2  beat
        Backbeat = 64, //the offBeat?? 
        HyperBeat = 128,// every 4 beats
        FirstAndThird = 256, // the 1 / 4 and 3 / 4
    }
}