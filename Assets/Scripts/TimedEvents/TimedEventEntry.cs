using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedEventEntry {
    public string ObserverName;
    public float TimeOffset;
    public GameObject TargetObject;
    public TimedEventType TypeOfEvent;
    public bool Triggered;

    public TimedEventEntry(string observerName, float timeOffset, GameObject targetObject, TimedEventType typeOfEvent, bool triggered)
    {
        this.ObserverName = observerName;
        this.TimeOffset = timeOffset;
        this.TargetObject = targetObject;
        this.TypeOfEvent = typeOfEvent;
        this.Triggered = triggered;
    }
}
