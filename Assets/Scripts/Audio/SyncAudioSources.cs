using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncAudioSources : MonoBehaviour {
    //set these in the inspector!
    public AudioSource master;
    public AudioSource slave;

    void Update()
    {
        slave.timeSamples = master.timeSamples;
    }

}
