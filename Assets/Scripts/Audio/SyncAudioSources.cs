using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncAudioSources : MonoBehaviour, ITimedEventSync {
    //set these in the inspector!
    public AudioSource master;
    public AudioSource slave;

    /// <summary>
    ///  Moves audio samples for master and slave to the offset speicifed, triggered from
    ///  TimedEventManager game object when synchronizing playback start.
    /// </summary>
    /// <param name="timeOffset"></param>
    public void MoveToTimeOffset(float timeOffset)
    {
        master.time = timeOffset;
        slave.time = timeOffset;
    }

    void Update()
    {
        slave.timeSamples = master.timeSamples;
    }

}
