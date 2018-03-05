using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

[System.Serializable]
public class Vec3Wrapper
{
    public JointType jointType;
    public HandState handState;
    public Vector3 vector = new Vector3();
}
