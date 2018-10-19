using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System.IO;

public class KinectKeyFramePlayer : KinectKeyFrameAnimation, ITimedEventSync {
    public bool Loop = false;
    public bool PlayOnAwake = true;
    public string SortingLayer = "Default";
    private bool m_isActive = false;
    private int m_keyFrameIndex = 0;
    public float xOffset = 0f;
    public float yOffset = 0f;

    void Start()
    {
        AssignSortingLayer();
        LoadAnimation();
        if (PlayOnAwake)
        {
            PlayAnimation();
        }
        else
        {
            StopAnimation();
        }
    }

    /// <summary>
    /// ITimedEventSync implementation, moves the keyframe index of this object to the nearest possible value 
    /// to the specified time offset.
    /// </summary>
    /// <param name="timeOffset"></param>
    public void MoveToTimeOffset(float timeOffset)
    {
        float m_keyFrameIndex = timeOffset / m_keyframeData.FrameSpeed;
    }

    void AssignSortingLayer()
    {
        var spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var renderer in spriteRenderers)
        {
            renderer.sortingLayerName = SortingLayer;
        }

        var lineRenderers = GetComponentsInChildren<LineRenderer>();
        foreach (var renderer in lineRenderers)
        {
            renderer.sortingLayerName = SortingLayer;
        }
    }

    public void PlayAnimation()
    {
        m_isActive = true;
    }

    public void StopAnimation()
    {
        m_isActive = false;
    }

    private void LoadAnimation()
    {
        m_keyFrameIndex = 0;
        string path = Path.Combine(Application.dataPath + AnimationDirectory, AnimationName + ".json");
        string json = File.ReadAllText(path);
        m_keyframeData = JsonUtility.FromJson<KeyframeData>(json);
    }

    private void AnimateFrame(int keyframe)
    {
        // Create a new set of targets based on the next key frame (starting them at the
        // position of our last targets)
        if (m_keyframeData.KeyFrames.Count > 0 && keyframe < m_keyframeData.KeyFrames.Count)
        {
            foreach(var jointData in m_keyframeData.KeyFrames[keyframe].vec3List)
            {
                // If we want to smooth our animations.
                Vector3 position = m_jointObjects[jointData.jointType].gameObject.transform.position;
                position.x = position.x + xOffset;
                position.y = position.y + yOffset;
                position = Vector3.Lerp(position, jointData.vector, m_keyframeData.FrameSpeed);
                m_jointObjects[jointData.jointType].gameObject.transform.position = position;
                m_jointObjects[jointData.jointType].GetComponent<KeyframeJoint>().handState = jointData.handState;
                
                // If we simply want to jump to the keyframe animation...
                // m_jointObjects[jointData.jointType].gameObject.transform.position = jointData.vector;
            }
        }
    }

    void Update()
    {
        if(!m_isActive)
        {
            return;
        }

        m_frameTimer += Time.deltaTime;
        if(m_frameTimer >= m_keyframeData.FrameSpeed)
        {
            m_frameTimer = 0f;
            m_keyFrameIndex++;
            if (m_keyFrameIndex > m_keyframeData.KeyFrames.Count)
            {
                if (!Loop)
                {
                    m_isActive = false;
                }
                else
                {
                    m_keyFrameIndex = 0;
                }
            }
            
        }

        AnimateFrame(m_keyFrameIndex);
    }
}
