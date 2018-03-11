using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddleScript : MonoBehaviour {

    public Transform pointA;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody>().MovePosition(pointA.position);
        Vector3 targetRotation = pointA.rotation.eulerAngles;
        targetRotation.z -= 90f;
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(targetRotation));
    }
}
