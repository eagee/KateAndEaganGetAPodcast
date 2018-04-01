using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnBroadcast : MonoBehaviour {
    public AudioSource Clip;
    public KinectKeyFramePlayer Player;

    void RecordingStarted()
    {
        Clip.Play();
        Player.PlayAnimation();
    }

}
