using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddleScript : MonoBehaviour {

    public GameObject rightHand;
    public GameObject leftHand;
    public PhysicMaterial BouncyMaterial;
    private float m_stateChangeTimer = 0f;
    private Windows.Kinect.HandState m_activeHandState;
    private KeyframeJoint m_jointL;
    private KeyframeJoint m_jointR;
    private Rigidbody m_rigidBody;

    // Use this for initialization
    void Start () {
        m_stateChangeTimer = 0f;
        m_activeHandState = Windows.Kinect.HandState.Open;
        m_jointL = leftHand.GetComponent<KeyframeJoint>();
        m_jointR = rightHand.GetComponent<KeyframeJoint>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void MovePaddleWithTarget(GameObject target, float rotateTargetBy)
    {
        m_rigidBody.isKinematic = true;
        m_rigidBody.MovePosition(target.transform.position);
        Vector3 targetRotation = target.transform.rotation.eulerAngles;
        targetRotation.z += rotateTargetBy;
        m_rigidBody.MoveRotation(Quaternion.Euler(targetRotation));
    }

    // Track the hand state, and only change our interpretation of that
    // hand state if we've seen a different hand state for over a second
    // *and* x and y of our transform are within 1f of the target game object.
    void FixedUpdate () {

        Vector3 distanceL = leftHand.transform.position - this.transform.position;
        Vector3 distanceR = rightHand.transform.position - this.transform.position;
        bool leftHandInRange = (Mathf.Abs(distanceL.x) <= 2f && Mathf.Abs(distanceL.y) <= 2f);
        bool rightHandInRange = (Mathf.Abs(distanceR.x) <= 2f && Mathf.Abs(distanceR.y) <= 2f);

        if ( (leftHandInRange && m_jointL && m_jointL.handState != m_activeHandState)
          || (rightHandInRange && m_jointR && m_jointR.handState != m_activeHandState) )
        {
            if (m_stateChangeTimer > 0f)
            {
                m_stateChangeTimer -= Time.deltaTime;
            }

            if(m_stateChangeTimer <= 0f)
            {
                if(leftHandInRange)
                    m_activeHandState = m_jointL.handState;
                else if(rightHandInRange)
                    m_activeHandState = m_jointR.handState;
            }
        }
        else
        {
            m_stateChangeTimer = 0.5f;
        }

        if (m_activeHandState == Windows.Kinect.HandState.Closed)
        {
            if(leftHandInRange)
            {
                GetComponent<Collider>().material = BouncyMaterial;
                MovePaddleWithTarget(leftHand, -90f);
            }
            else if (rightHandInRange)
            {
                GetComponent<Collider>().material = BouncyMaterial;
                MovePaddleWithTarget(rightHand, -90f);
            }
            else
            {
                GetComponent<Collider>().material = null;
            }
        }
        else
        {
            m_rigidBody.isKinematic = false;
        }
    }
}
