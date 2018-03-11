using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawPaddleString : MonoBehaviour
{
    public Transform transformFrom;
    public int OrderInLayer;
    public string SortingLayer = "Default";
    public float ZOffset = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<LineRenderer>().sortingOrder = OrderInLayer;
        this.GetComponent<LineRenderer>().sortingLayerName = SortingLayer;
        Vector3 fromPosition = transformFrom.position;
        fromPosition.z += ZOffset;
        this.GetComponent<LineRenderer>().SetPosition(0, fromPosition);
        Vector3 toPosition = this.transform.position;
        toPosition.z += ZOffset;
        this.GetComponent<LineRenderer>().SetPosition(1, toPosition);
    }
}
