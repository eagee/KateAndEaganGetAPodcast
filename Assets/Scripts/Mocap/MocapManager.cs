using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MocapManager : MonoBehaviour {
    public GameObject targetPrefab;
    public string AnimationDirectory = "Q:\\TeamSheeps\\KateAndEaganGetAPodcast\\KateAndEaganGetAPodcast\\Assets\\MocapRecordings";
    public List<string> KeyframeAnimations;
    private KeyframeData m_ActiveData = new KeyframeData();
    private int m_listIndex = 0;
    private int m_keyFrameIndex = 0;
    private List<GameObject> m_ActiveTargets;
    private float m_currentScore;

    private float m_LastStarfieldPosY;
    float countdownToPoint;
    public int winningScore = 100;

    void Awake()
    {
    }

    private void SetupNextDanceAnimation()
    {
        m_keyFrameIndex = 0;
        string path = Path.Combine(AnimationDirectory, KeyframeAnimations[m_listIndex]);
        string json = File.ReadAllText(path);
        m_ActiveData = JsonUtility.FromJson<KeyframeData>(json);
        CreateTargetsForKeyFrame(m_keyFrameIndex);
    }

    // Use this for initialization
    void Start () {
        m_currentScore = 0;
        m_ActiveTargets = new List<GameObject>();
        m_listIndex = 0;
        SetupNextDanceAnimation();
    }

    public int GetCurrentScore()
    {
        return (int)m_currentScore;
    }
    
    void CreateTargetsForKeyFrame(int keyframe)
    {
        // Create a copy of the targets for the last frame so that we can
        // use them as our starting positions
        List<GameObject> lastActiveTargets = new List<GameObject>();
        foreach (GameObject obj in m_ActiveTargets)
        {
            lastActiveTargets.Add(obj);
        }
        m_ActiveTargets.Clear();

        // Create a new set of targets based on the next key frame (starting them at the
        // position of our last targets)
        if (m_ActiveData.KeyFrames.Count > 0 && keyframe < m_ActiveData.KeyFrames.Count)
        {
            for(int index = 0; index < m_ActiveData.KeyFrames[keyframe].vec3List.Count; index++)
            {
                Vector3 startingPosition;
                if (lastActiveTargets.Count == 0)
                {
                    startingPosition = m_ActiveData.KeyFrames[keyframe].vec3List[index].vector;
                }
                else
                {
                    startingPosition = lastActiveTargets[index].transform.position;
                }
                GameObject newTarget = Instantiate(targetPrefab, startingPosition, Quaternion.identity);
                Vector3 targetPosition = m_ActiveData.KeyFrames[keyframe].vec3List[index].vector;
                newTarget.GetComponent<TargetScript>().TargetPostion = targetPosition;
                m_ActiveTargets.Add(newTarget);
            }
        }

        // Destroy the last active targets, we no longer need them
        foreach (GameObject obj in lastActiveTargets)
        {
            obj.GetComponent<TargetScript>().HandleDestruction();
        }
        lastActiveTargets.Clear();
    }
    
    
	// Update is called once per frame
	void Update ()
    {

    }
}
