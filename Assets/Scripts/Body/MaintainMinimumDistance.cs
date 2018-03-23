using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainMinimumDistance : MonoBehaviour {
    public Transform TargetObject;
    public float XDistance = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = TargetObject.transform.position;
        Vector3 myPos = this.transform.position;
        
        // If we're to the right of the target object
        if (targetPos.x < myPos.x) 
        {
            float distance = myPos.x - targetPos.x;
            if(distance < XDistance)
            {
                myPos.x += XDistance - distance;
                this.transform.position = myPos;
            }
        }
        else if (targetPos.x > myPos.x)
        {
            float distance = targetPos.x - myPos.x;
            if (distance < XDistance)
            {
                myPos.x -= XDistance - distance;
                this.transform.position = myPos;
            }
        }
    }
}
