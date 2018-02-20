using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class KeyframeData
{
    public string Name;
    public Vec3Wrapper Offset = new Vec3Wrapper();
    public float Scale = 0.0f;
    public float FrameSpeed = 0.16f;
    public List<Vec3ListWrapper> KeyFrames = new List<Vec3ListWrapper>();
}
