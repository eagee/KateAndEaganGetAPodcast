using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class JointTracker : MonoBehaviour
{
    public JointType JointToUse;
    public BodySourceManager _bodyManager;
    public float scale = 8f;
    public float yOffset = -0.1f;
    public int ActiveBodyNumber = 0;
    public bool useZValue = false;

    private KinectJointFilter m_jointFilter;
    private HandState m_leftHandState;
    private HandState m_rightHandState;

    void Awake()
    {
        m_jointFilter = new KinectJointFilter();
        m_jointFilter.Init(0.55f, 0.25f, 2.0f, 0.30f, 1.25f);
    }

    void Start()
    {
        m_leftHandState = HandState.Open;
        m_rightHandState = HandState.Open;
    }

    public HandState handState
    {
        get
        {
            if(JointToUse == JointType.HandLeft)
            {
                return m_leftHandState;
            }
            else if(JointToUse == JointType.HandRight)
            {
                return m_rightHandState;
            }
            else
            {
                return HandState.Open;
            }
        }
    }

    public bool leftHandClosed
    {
        get
        {
            return m_leftHandState == HandState.Closed;
        }
    }

    public bool rightHandClosed
    {
        get
        {
            return m_rightHandState == HandState.Closed;
        }
    }


    // Get body data from the body manager and track the joint for the active body
    void Update()
    {
        if (_bodyManager == null)
        {
            return;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return;
        }

        // Use for actual multi-player environments!
        if ((data.Length >= ActiveBodyNumber) && (data[ActiveBodyNumber] != null) && (data[ActiveBodyNumber].IsTracked))
        {
            GetComponent<Rigidbody>().isKinematic = true;

            m_jointFilter.UpdateFilter(data[ActiveBodyNumber]);
            var Joints = m_jointFilter.GetFilteredJoints();

            m_leftHandState = data[ActiveBodyNumber].HandLeftState;
            m_rightHandState = data[ActiveBodyNumber].HandRightState;

            // Grab the mid spine position, we'll use this to make all other joint movement relative to the spine (this way we can limit the Y position of the character)
            var midSpinePosition = Joints[(int)JointType.SpineMid];
            var jointPos = Joints[(int)JointToUse];
            //jointPos.X -= midSpinePosition.X;
            //jointPos.Y -= midSpinePosition.Y;
            jointPos.Z -= midSpinePosition.Z;

            float zValue = (useZValue == true) ? jointPos.Z : 0f;


            Vector3 targetPosition = new Vector3((midSpinePosition.X + jointPos.X) * scale, ((yOffset + jointPos.Y) * scale), zValue);


            this.transform.position = targetPosition;
        }
        else
        {
            // Hide the object by moving it far away from the camera.
            this.transform.position = new Vector3(-100f, -100f, 0f);

            // Attempt to find the active body number by iterating through the current bodies, finding a relevant body, and then assigning the active body. Once we have one
            // the user will be reacting to it from that point forward.
            int bodyIndex = 0;
            foreach (Body body in data)
            {
                if ((body != null) && (body.IsTracked))
                {
                    ActiveBodyNumber = bodyIndex;
                    break;
                }
                bodyIndex++;
            }
        }
    }
}
