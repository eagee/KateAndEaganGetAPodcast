using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    private JointTracker m_jointTracker;
    public bool LeftHand = true;

    // Use this for initialization
    void Start ()
    {
        m_jointTracker = GetComponent<JointTracker>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "CanPickUp")
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftHand)
        {
            if (m_jointTracker.leftHandClosed)
            {
                //GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
            }
            else
            {
                //GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Simple;
            }
        }
        else
        {
        }
        
    }
}
