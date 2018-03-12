using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

/// This class allows us to specify a joint that can be used by both KinectKeyFrameRecorder and KinectKeyFramePlayer
/// by attaching this script to an object and indicating the joint it will represent. If a Kinect JointTracker object
/// is already present on the object, the joint will be set to the joint being used by JointTracker.
[ExecuteInEditMode]
public class KeyframeJoint : MonoBehaviour {

    public JointType JointToUse;
    private HandState m_handState;
    private JointTracker m_activeTracker;

    public HandState handState
    {
        get { return m_handState; }
        set { m_handState = value; }
    }

    void Start () {
        m_activeTracker = GetComponent<JointTracker>();
        m_handState = HandState.Open;
	}

    void Update() {
        // This only works for an active kinect model, otherwise handset is specified by the playback script.
        if (m_activeTracker != null)
        {
            if (m_activeTracker.JointToUse == JointToUse)
            {
                m_handState = m_activeTracker.handState;
            }
        }
    }
}
