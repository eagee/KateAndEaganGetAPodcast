using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampVelocityScript : MonoBehaviour {
    private Rigidbody m_rigidBody;
    
    // Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        m_rigidBody.velocity = Vector3.ClampMagnitude(m_rigidBody.velocity, 20f);
    }
}
