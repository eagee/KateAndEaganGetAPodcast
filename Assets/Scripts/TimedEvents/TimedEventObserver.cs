using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;

[System.Serializable]
public class TimedEventObserver : MonoBehaviour, ITimedEventObserver
{
    [SerializeField]
    public string ObserverName;

    // Method called when TimedEventManager sends LookAtObject message
    public void LookAtObject(GameObject targetObject)
    {
        GetComponent<RandomEyes2D>().SetLookTarget(targetObject);
    }

    // Method called when TimedEventMangager sends LookRandom message
    public void LookRandom()
    {
        GetComponent<RandomEyes2D>().SetLookTarget(null);
    }
};
