using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.IO;

/// KinectKeyFrameRecorder records positional information from JointTracker child objects to json
/// which can be used later to play back the recorded animation.
public class KinectKeyFrameRecorder : KinectKeyFrameAnimation
{
    public float FrameTime = 1.0f / 8.0f;
    public bool IsRecording = false;
    private bool m_LastRecordingState = false;
    public float delayBeforeRecording = 10f;

    // Use this for initialization
    void Start()
    {
        m_keyframeData.FrameSpeed = FrameTime;
        IsRecording = false;
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
                m_LastRecordingState = false;
            }
            else if (m_LastRecordingState == false && IsRecording)
            {
                if (delayBeforeRecording > 0f)
                { 
                    delayBeforeRecording -= Time.deltaTime;
                    if(delayBeforeRecording <= 0f)
                    {
                        BroadcastMessage("RecordingStarted");
                    }
                }
                else
                { 
                    m_LastRecordingState = true;
                }
            }
        }

        if (m_LastRecordingState && m_frameTimer >= FrameTime)
        {
            m_frameTimer = 0;
            RecordKeyframe();
        }
    }

    private void RecordKeyframe()
    {
        Vec3ListWrapper newKeyFrame = new Vec3ListWrapper();

        // Iterate through each joint and save the position of each joint to the specified keyframe.
        foreach (KeyValuePair<JointType, GameObject> jointData in m_jointObjects)
        {
            Vec3Wrapper vec3 = new Vec3Wrapper();
            vec3.vector = jointData.Value.transform.position;
            vec3.jointType = jointData.Key;
            vec3.handState = jointData.Value.GetComponent<JointTracker>().handState;
            newKeyFrame.vec3List.Add(vec3);
        }

        m_keyframeData.KeyFrames.Add(newKeyFrame);
    }

    private void SaveKeyframesToJson()
    {
        string filePath = Application.dataPath + AnimationDirectory + AnimationName + ".json";
        Debug.Log("Writing key frame file to: " + filePath);
        string json = JsonUtility.ToJson(m_keyframeData);

        while(File.Exists(filePath))
        {
            filePath = Application.dataPath + AnimationDirectory + AnimationName + Random.Range(0, 10000).ToString() + ".json";
        }

        File.WriteAllText(filePath, json);
        Debug.Log("Finished writing key frame file to: " + filePath);
    }
}
