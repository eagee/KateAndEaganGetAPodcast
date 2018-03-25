using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timeline events keep a timer running throughout the scene and send messages to objects
[System.Serializable]
public class TimedEventManager : MonoBehaviour {
    public List<TimedEventEntry> EvenEntries;
    public List<GameObject> Observers;
    private float m_EventTimer;

    void Update()
    {                            
        m_EventTimer += Time.deltaTime;
        GetComponent<TextMesh>().text = m_EventTimer.ToString();

        foreach(var entry in EvenEntries)
        {
            if(m_EventTimer > entry.TimeOffset && entry.Triggered == false)
            {
                entry.Triggered = true;
                foreach(var observer in Observers)
                {
                    if(observer.name == entry.ObserverName)
                    { 
                        if(entry.TypeOfEvent == TimedEventType.LookAtObject)
                        {
                            observer.SendMessage("LookAtObject", entry.TargetObject);
                        }
                        else if (entry.TypeOfEvent == TimedEventType.LookRandom)
                        {
                            observer.SendMessage("LookRandom");
                        }
                    }
                }
            }
        }
    }
}
