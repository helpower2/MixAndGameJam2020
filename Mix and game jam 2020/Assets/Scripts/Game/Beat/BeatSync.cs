using UnityEngine;

namespace Game.Beat
{
    public class BeatSync : MonoBehaviour, IBeat
    {
        public void HalfBeat(int index)
        {
            //you're code here
        }

        public void Beat(int index)//called every beat
        {
            if (BeatIndex.IsBeat(BeatType.Downbeat))// checks if beat is downbeat
            {
                //you're code here
            }
        }
    }
}
