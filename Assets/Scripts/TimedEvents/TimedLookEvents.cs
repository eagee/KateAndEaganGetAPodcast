using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;

public class TimedLookEvents : MonoBehaviour, ITimedLookEvents
{
    
    public void LookAtObject(GameObject targetObject)
    {
        GetComponent<RandomEyes2D>().SetLookTarget(targetObject);
    }

    public void LookRandom()
    {
        GetComponent<RandomEyes2D>().SetLookTarget(null);
    }
};
