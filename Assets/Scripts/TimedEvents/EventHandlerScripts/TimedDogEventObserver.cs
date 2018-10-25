using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimedDogEventObserver : TimedEventObserver {

    public Transform TargetLocation;
    public float Speed = 1.0f;

    private bool m_Active;
    
	// Use this for initialization
	void Start () {
        m_Active = false;
    }
    
    new public void Activate()
    {
        m_Active = true;
    }

    // Update is called once per frame
    void Update () {
        if(m_Active == true)
        {
            var pos = this.transform.position;
            pos = Vector3.Lerp(this.transform.position, TargetLocation.position, Speed * Time.deltaTime);
            this.transform.position = pos;
            if(this.transform.position == TargetLocation.position)
            {
                GameObject.Destroy(this);
            }
        }
	}
}
