namespace Game.Beat
{
    public class PickupBeat : Beat
    {
        public GameManager child1;
        public GameManager child2;
        private bool active;
        private int moveIndex;
        public bool[] platformBeat;
        public override void OnBeat(int beat)
        {
            moveIndex++;
            active = platformBeat[moveIndex % platformBeat.Length];
            //tag = active ? platformTag : voidTag;
        }
    }
}