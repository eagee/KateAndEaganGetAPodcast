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

    void Start () {
        JointTracker activeTracker = GetComponent<JointTracker>();
        if(activeTracker != null)
        {
            JointToUse = activeTracker.JointToUse;
        }
	}
}
