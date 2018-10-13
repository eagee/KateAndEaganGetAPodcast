using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimedEventEntry {
    public TimedEventObserver Observer;
    public float TimeOffset;
    public TimedEventTarget TargetObject;
    public TimedEventType TypeOfEvent;
    public bool Triggered;

    public TimedEventEntry(TimedEventObserver observer, float timeOffset, TimedEventTarget targetObject, TimedEventType typeOfEvent, bool triggered)
    {
        this.Observer = observer;
        this.TimeOffset = timeOffset;
        this.TargetObject = targetObject;
        this.TypeOfEvent = typeOfEvent;
        this.Triggered = triggered;
    }
}
