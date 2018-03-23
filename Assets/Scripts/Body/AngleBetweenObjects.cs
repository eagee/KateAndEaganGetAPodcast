using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AngleBetweenObjects : MonoBehaviour {

    public Transform target;
    public float speed = 1.0f;
    public float mAngle = 0f;
    public float clampMin = -360f;
    public float clampMax = 360f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame  
	void Update () {
        Vector3 vectorToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, clampMin, clampMax);
        mAngle = angle;
        Quaternion q = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}
