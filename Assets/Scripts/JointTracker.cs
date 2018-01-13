using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class JointTracker : MonoBehaviour
{

    public JointType JointToUse;
    public BodySourceManager _bodyManager;
    public float scale = 8f;
    public float yOffset = -5f;
    public int BodyNumber = 0;
    public bool useZValue = false;

    private KinectJointFilter m_jointFilter;

    void Awake()
    {
        m_jointFilter = new KinectJointFilter();
        m_jointFilter.Init(0.55f, 0.25f, 2.0f, 0.30f, 1.25f);
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
        if ((data.Length >= BodyNumber) && (data[BodyNumber] != null) && (data[BodyNumber].IsTracked))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            
            m_jointFilter.UpdateFilter(data[BodyNumber]);
            var Joints = m_jointFilter.GetFilteredJoints();

            // Grab the mid spine position, we'll use this to make all other joint movement relative to the spine (this way we can limit the Y position of the character)
            var midSpinePosition = Joints[(int)JointType.SpineMid];
            var jointPos = Joints[(int)JointToUse];
            //jointPos.X -= midSpinePosition.X;
            //jointPos.Y -= midSpinePosition.Y;
            jointPos.Z -= midSpinePosition.Z;

            float zValue = (useZValue == true) ? jointPos.Z : 0f;

            Vector3 targetPosition = new Vector3((midSpinePosition.X + jointPos.X) * scale, (yOffset + jointPos.Y) * scale, zValue);
            this.transform.position = targetPosition;
        }
        else
        {
            //GetComponent<Rigidbody>().isKinematic = false;
            //var pos = this.transform.position;
            //if (pos.y <= -10f)
            //{
            //    pos.x = -10;
            //    pos.y = -10f;
            //    this.transform.position = pos;
            //}

            this.transform.position = new Vector3(100.0f, 100.0f, 100.0f);
        }
    }
}
