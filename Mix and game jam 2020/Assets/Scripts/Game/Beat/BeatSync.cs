using UnityEngine;

namespace Game.Beat
{
    public class BeatSync : MonoBehaviour, IBeat, GameManager.IReset
    {
        public BeatType beatType;
        public Sprite spikesSprite;
        private SpriteRenderer spriteRenderer;
        private Sprite defaultSprite;
        private PolygonCollider2D polygonCollider2D;
        private bool active;
        private int moveIndex;
        public bool[] platformBeat;
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSprite = spriteRenderer.sprite;

            polygonCollider2D = GetComponent<PolygonCollider2D>();
            polygonCollider2D.enabled = false;
        }

        public void HalfBeat(int index)
        {
            //you're code here
        }

        public void Beat(int index) //called every beat
        {
            if (!BeatIndex.IsBeat(beatType))
                return;
            if (platformBeat.Length != 0)
            {
                active = platformBeat[moveIndex % platformBeat.Length];
            }
            else
            {
                active = !active;
            }

            moveIndex++;
            polygonCollider2D.enabled = active;
            spriteRenderer.sprite = (active) ? spikesSprite : defaultSprite;
        }

        public void WorldReset()
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }
}
