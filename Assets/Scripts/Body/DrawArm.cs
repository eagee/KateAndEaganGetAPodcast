using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawArm : MonoBehaviour
{
    public Transform transformFrom;
    public int OrderInLayer;
    //public string SortingLayer = "Default";
    public float ZOffset = 0f;
    public bool isBicep = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isBicep)
        {
            this.GetComponent<LineRenderer>().sortingOrder = OrderInLayer;
            //this.GetComponent<LineRenderer>().sortingLayerName = SortingLayer;
            this.GetComponent<LineRenderer>().SetPosition(0, transformFrom.position);
            Vector3 endOfSleeve = Vector3.Lerp(transformFrom.position, this.transform.position, 0.75f);
            endOfSleeve.z = transformFrom.position.z;
            this.GetComponent<LineRenderer>().SetPosition(1, endOfSleeve);
            this.GetComponent<LineRenderer>().SetPosition(2, this.transform.position);
        }
        else
        {
            this.GetComponent<LineRenderer>().sortingOrder = OrderInLayer;
            //this.GetComponent<LineRenderer>().sortingLayerName = SortingLayer;
            this.GetComponent<LineRenderer>().SetPosition(0, transformFrom.position);
            Vector3 toPosition = this.transform.position;
            toPosition.z += ZOffset;
            this.GetComponent<LineRenderer>().SetPosition(1, toPosition);
        }
        
    }
}
