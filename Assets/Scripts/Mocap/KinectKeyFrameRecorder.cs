using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.IO;

/// KinectKeyFrameRecorder records positional information from JointTracker child objects to json
/// which can be used later to play back the recorded animation.
public class KinectKeyFrameRecorder : MonoBehaviour {
    public string AnimationName = "";
    public string AnimationDirectory = "Q:\\TeamSheeps\\KateAndEaganGetAPodcast\\KateAndEaganGetAPodcast\\Assets\\MocapRecordings";
    public float FrameTime = 1.0f / 8.0f;
    private Dictionary<JointType, GameObject> m_jointObjects;
    private KeyframeData m_keyframeData;
    public bool IsRecording = false;
    private bool m_LastRecordingState = false;
    private float m_frameTimer;

    // Use this for initialization
    void Start()
    {
        IsRecording = false;
        m_jointObjects = new Dictionary<JointType, GameObject>();
        m_keyframeData = new KeyframeData();
        m_keyframeData.Name = "TestAnimation";
        var joints = this.GetComponentsInChildren<JointTracker>(false);
        foreach (JointTracker j in joints)
        {
            m_jointObjects[j.JointToUse] = j.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_frameTimer += Time.deltaTime;
        if(m_LastRecordingState != IsRecording)
        {
            if(m_LastRecordingState == true && IsRecording == false)
            {
                SaveKeyframesToJson();
            }
            m_LastRecordingState = IsRecording;
        }
        if (IsRecording && m_frameTimer >= FrameTime)
        {
            m_frameTimer = 0;
            RecordKeyframe();
        }
    }

    private void RecordKeyframe()
    {
        Vec3ListWrapper newKeyFrame = new Vec3ListWrapper();

        // Iterate through each joint and output code to console with initialized vector3 list
        foreach (KeyValuePair<JointType, GameObject> jointData in m_jointObjects)
        {
            Vec3Wrapper vec3 = new Vec3Wrapper();
            vec3.vector = jointData.Value.transform.position;
            newKeyFrame.vec3List.Add(vec3);
        }

        m_keyframeData.KeyFrames.Add(newKeyFrame);
        Debug.Log("Added new keyframe!");
    }

    private void SaveKeyframesToJson()
    {
        string filePath = AnimationDirectory + AnimationName + ".json";
        Debug.Log("Writing keyframe file to: " + filePath);
        string json = JsonUtility.ToJson(m_keyframeData);

        while(File.Exists(filePath))
        {
            filePath = AnimationDirectory + AnimationName + Random.Range(0, 10000).ToString() + ".json";
        }

        File.WriteAllText(filePath, json);
    }
}
