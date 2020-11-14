using UnityEngine;

namespace Game.Beat
{
    public class BeatSync : MonoBehaviour, IBeat
    {
        public Sprite spikesSprite;
        private SpriteRenderer spriteRenderer;
        private Sprite defaultSprite;
        private PolygonCollider2D polygonCollider2D;

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

        public void Beat(int index)//called every beat
        {
            if (BeatIndex.IsBeat(BeatType.Downbeat))
            {
                Debug.Log("DownBeat");
                spriteRenderer.sprite = spikesSprite;
                polygonCollider2D.enabled = true;
            }
            if (BeatIndex.IsBeat(BeatType.OnBeat))
            {
                Debug.Log("OnBeat");
                spriteRenderer.sprite = defaultSprite;
                polygonCollider2D.enabled = false;
            }
        }
    }
}
