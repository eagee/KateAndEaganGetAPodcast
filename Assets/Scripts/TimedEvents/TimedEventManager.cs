using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Timeline events keep a timer running throughout the scene and sends messages to objects when their timeout is hit.
///  Also keeps a list of objects to syncrhonize to the start timer offset in this object.
/// </summary>
[System.Serializable]
[RequireComponent(typeof(TimedEventSyncObservers))]
public class TimedEventManager : MonoBehaviour {
    // The starting offset
    public float StartingTimeOffset = 0f;
    public List<TimedEventEntry> EventEntries;
    private float m_EventTimer;

    private void Start()
    {
        m_EventTimer = StartingTimeOffset;
        foreach(var observer in GetComponent<TimedEventSyncObservers>().TimedEventSyncObjects)
        {
            observer.Result.MoveToTimeOffset(StartingTimeOffset);
        }
    }

    void Update()
    {                            
        m_EventTimer += Time.deltaTime;
        GetComponent<TextMesh>().text = m_EventTimer.ToString();

        foreach(var entry in EventEntries)
        {
            if(m_EventTimer > entry.TimeOffset && entry.Triggered == false)
            {
                entry.Triggered = true;
                switch(entry.TypeOfEvent)
                {
                    case TimedEventType.LookAtObject:
                        entry.Observer.SendMessage("LookAtObject", entry.TargetObject.gameObject);
                        break;
                    case TimedEventType.LookRandom:
                        entry.Observer.SendMessage("LookRandom");
                        break;
                    case TimedEventType.EmoteNormal:
                        entry.Observer.SendMessage("EmoteNormal");
                        break;
                    case TimedEventType.EmoteAmusement:
                        entry.Observer.SendMessage("EmoteAmusement");
                        break;
                    case TimedEventType.EmoteNonplussed:
                        entry.Observer.SendMessage("EmoteNonplussed");
                        break;
                }
            }
        }
    }
}
