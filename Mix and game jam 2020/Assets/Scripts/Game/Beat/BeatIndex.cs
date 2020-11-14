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
        public static bool IsBeat(BeatType beatType)
        {
            switch (beatType)
            {
                case BeatType.Downbeat:
                    return BeatTypes[0];
                case BeatType.Second:
                    return BeatTypes[1];                
                case BeatType.OnBeat:
                    return BeatTypes[2];                
                case BeatType.Fourth:
                    return BeatTypes[3];                
                case BeatType.EighthBeat:
                    return BeatTypes[4];                
                case BeatType.Offbeat:
                    return BeatTypes[5];                
                case BeatType.Backbeat:
                    return BeatTypes[6];                
                case BeatType.HyperBeat:
                    return BeatTypes[7];
                case BeatType.FirstAndThird:
                    return BeatTypes[8];
                default:
                    return false;
            }
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
    public enum BeatType
    {
        Downbeat, //the first beat 1 / 4
        Second, //the second beat 2 / 4
        OnBeat, //the third beat 3 / 4 
        Fourth, //the fourth beat 4 / 4 
        EighthBeat, //the eighth beat 8 / 8
        Offbeat, //ever 1.5 / 2  beat
        Backbeat, //the offBeat?? 
        HyperBeat,// every 4 beats
        FirstAndThird,
    }
}