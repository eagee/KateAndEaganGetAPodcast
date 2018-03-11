using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

//[ExecuteInEditMode]
public class PaddleBallScript : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    public Transform targetC;
    public KeyframeJoint handJoint;
    public float Speed = 1f;
    
    private float m_distance = 0f;
    private Transform m_currentTarget;
    public bool isBouncing = true;
    private bool m_lastBouncingState = true;
    private bool switchedTargets = false;

    // Use this for initialization
    void Start()
    {
        m_currentTarget = targetA;
        m_distance = 0f;
    }

    private void BounceBall()
    {
        switchedTargets = false;
        Vector3 toPosition = this.transform.position;
        if (m_currentTarget == targetA)
        {
            toPosition = Vector3.Slerp(this.transform.position, targetA.transform.position, m_distance);
        }
        else
        {
            toPosition = Vector3.Lerp(this.transform.position, targetB.transform.position, m_distance);
        }
        m_distance += Time.deltaTime * Speed;
        if (m_distance >= 0.9f)
        {
            m_distance = 0f;
            if (m_currentTarget == targetA)
                m_currentTarget = targetB;
            else
                m_currentTarget = targetA;
            switchedTargets = true;
        }
        this.transform.position = toPosition;
        targetC.GetComponent<Rigidbody>().isKinematic = true;
        targetC.GetComponent<SpringJoint>().spring = 0;
        targetC.transform.position = this.transform.position;
    }

    private void DangleBall()
    {
        switchedTargets = false;
        Vector3 toPosition = this.transform.position;
        toPosition = Vector3.Lerp(this.transform.position, targetC.transform.position, Time.deltaTime * Speed);
        this.transform.position = toPosition;
        targetC.GetComponent<Rigidbody>().isKinematic = false;
        targetC.GetComponent<SpringJoint>().spring = 30;
    }

    private void DrawRubberBand()
    {
        this.GetComponent<LineRenderer>().SetPosition(0, targetB.position);
        Vector3 offsetPosition = this.transform.position;
        //offsetPosition.z -= 5;
        this.GetComponent<LineRenderer>().SetPosition(1, offsetPosition);
    }

    // Update is called once per frame
    void Update()
    {

        isBouncing = handJoint.handState == HandState.Closed;
        DrawRubberBand();

        if (m_lastBouncingState != isBouncing)
        {
            if (isBouncing == false)
            {
                if (m_currentTarget == targetB && switchedTargets)
                {
                    m_distance = 0f;
                    m_lastBouncingState = isBouncing;
                }
            }
            else
            {
                m_distance = 0f;
                m_currentTarget = targetB;
                m_lastBouncingState = isBouncing;
            }

        }

        if (isBouncing)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            BounceBall();
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            DangleBall();
        }

    }
}
