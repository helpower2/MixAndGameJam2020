using System.Linq;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

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
        
        [HideInInspector] public int bpm;
        [HideInInspector] public float timeOfLastBeat;
        [HideInInspector] public float timeOfLastHalfBeat;
        
        private float Time => musicManager.audioSource.time;
        private readonly List<IBeat> _beatSyncedObjects = new List<IBeat>(); //the list of IBeat objects 

        private void Awake()
        {
            musicManager = musicManager ?? FindObjectOfType<MusicManager>();
            BeatIndex.BeatsPerMinute = bpm;
            var iBeatObjects = FindObjectsOfType<MonoBehaviour>().OfType<IBeat>();
            _beatSyncedObjects.AddRange(iBeatObjects);
        }
        
        // Update is called once per frame
        void Update()
        {
            if (!running) return; //return when not running

            if (Time - timeOfLastBeat <= BeatIndex.SecondsPerBeat)
            {
                //not yet time for a beat 
                //check half beat
                if (Time - timeOfLastHalfBeat <= BeatIndex.SecondsPerHalfBeat) return;

                //time for a Half beat
                BeatIndex.CalculateHalfBeat();
                timeOfLastHalfBeat = Time;
                _beatSyncedObjects.ForEach(x => x.HalfBeat(beatIndex));

                return;
            }

            beatIndex++;
            BeatIndex.CalculateBeat(beatIndex);
            timeOfLastBeat = Time;
            _beatSyncedObjects.ForEach(x => x.Beat(beatIndex));
        }

        /// <summary>
        ///start or stops/resets the beat master
        /// </summary>
        [Button]
        public void StartStopBeat()
        {
            running = !running;//starts or stops
            if (running) return;//when running return
            musicManager.audioSource.Stop();
            beatIndex = 0; //reset beat index
            timeOfLastBeat = 0f;
            timeOfLastHalfBeat = 0f;
        }
        public void ResetBeat()
        {
            beatIndex = 0; //reset beat index
            timeOfLastBeat = 0f;
            timeOfLastHalfBeat = 0f;
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
}


