using System.Linq;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine; 


#if UNITY_EDITOR

using UnityEditor;
#endif

namespace Game.Beat
{
    /// <summary>
    /// the beat master
    /// this makes IBeat work 
    /// </summary>
    /// <remarks>only one per scene is allowed</remarks>
    public class BeatMaster : MonoBehaviour
    {
        public int beatIndex;
        public bool running = false;
        public MusicManager musicManager;
        
        //[HideInInspector] public int bpm;
        [HideInInspector] public float timeOfLastBeat;
        [HideInInspector] public float timeOfLastHalfBeat;
        
        private float Time => musicManager.audioSource.time;
        private readonly List<IBeat> _iBeatSyncedObjects = new List<IBeat>(); //the list of IBeat objects 
        private readonly List<Beat> _beatSyncedObjects = new List<Beat>(); //the list of IBeat objects 

        private void Awake()
        {
            musicManager = musicManager ?? FindObjectOfType<MusicManager>();
            var iBeatObjects = FindObjectsOfType<MonoBehaviour>().OfType<IBeat>();
            var beatObjects = FindObjectsOfType<MonoBehaviour>().OfType<Beat>();
            _iBeatSyncedObjects.AddRange(iBeatObjects);
            _beatSyncedObjects.AddRange(beatObjects);
        }
        
        // Update is called once per frame
        void Update()
        {
            if (!running) return; //return when not running
            if (Time < timeOfLastBeat) timeOfLastBeat = 0;
                if (Time - timeOfLastBeat <= BeatIndex.SecondsPerBeat)
            {
                //not yet time for a beat 
                //check half beat
                if (Time - timeOfLastHalfBeat <= BeatIndex.SecondsPerHalfBeat) return;

                //time for a Half beat
                BeatIndex.CalculateHalfBeat();
                timeOfLastHalfBeat = Time;
                _iBeatSyncedObjects.ForEach(x => x.HalfBeat(beatIndex));

                return;
            }

            beatIndex++;
            BeatIndex.CalculateBeat(beatIndex);
            timeOfLastBeat = Time;
            _iBeatSyncedObjects.ForEach(x => x.Beat(beatIndex));
            AbstractBeat(beatIndex);
        }

        /// <summary>
        ///start or stops/resets the beat master
        /// </summary>
        public void StartBeat()
        {
            running = true;//starts or stops
        }

        public void Stopbeat()
        {
            running = false;
        }
        public void ResetBeat()
        {
            beatIndex = 0; //reset beat index
            timeOfLastBeat = 0f;
            timeOfLastHalfBeat = 0f;
        }

        private void AbstractBeat(int beatIndex)
        {
            foreach (var beat in _beatSyncedObjects)
            {
                if (!running)
                    continue;
                
                if (BeatIndex.IsBeat(beat.beatType, beat.offset))
                    beat.OnBeat(beatIndex);

            }
        }
    }

    /// <summary>
    /// used to implement the Beat and HalfBeat callbacks  
    /// </summary>
    public interface IBeat
    {   
        /// <summary>
        /// called every beat
        /// </summary>
        /// <param name="index">the beat index</param>
        void Beat(int index);
        
        /// <summary>
        /// called ever half beat
        /// </summary>
        /// <param name="index">the beat index</param>
        void HalfBeat(int index);
    }

    public abstract class Beat : MonoBehaviour
    {
        public int beatMultiplier = 1;
        public int offset = 0;
        public BeatType beatType;

        public abstract void OnBeat(int beat);
#if UNITY_EDITOR
        private void OnGUI()
        {
            beatType = (Game.Beat.BeatType) EditorGUILayout.EnumFlagsField(beatType);
        }
#endif

    }
}


