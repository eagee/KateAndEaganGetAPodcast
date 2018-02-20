using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;

public class KinectKeyFrameAnimation : MonoBehaviour {
    public string AnimationName = "";
    public string AnimationDirectory = "/StreamingAssets/Mocap/";
    protected Dictionary<JointType, GameObject> m_jointObjects;
    protected KeyframeData m_keyframeData;
    protected float m_frameTimer;

    // Set up the joints that our key frame animation will use.
    void Awake()
    {
        m_keyframeData = new KeyframeData();
        m_keyframeData.Name = (AnimationName != "") ? AnimationName : "TestAnimation";

        m_jointObjects = new Dictionary<JointType, GameObject>();
        var joints = this.GetComponentsInChildren<KeyframeJoint>(false);
        foreach (KeyframeJoint j in joints)
        {
            m_jointObjects[j.JointToUse] = j.gameObject;
        }
    }
}
