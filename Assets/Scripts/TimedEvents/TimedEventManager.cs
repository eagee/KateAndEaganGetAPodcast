using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Timeline events keep a timer running throughout the scene and send messages to objects
[System.Serializable]
public class TimedEventManager : MonoBehaviour {
    public List<TimedEventEntry> EventEntries;
    public List<TimedEventObserver> Observers;
    private float m_EventTimer;
    
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
