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
                if (entry.TypeOfEvent == TimedEventType.LookAtObject)
                {
                    entry.Observer.SendMessage("LookAtObject", entry.TargetObject);
                }
                else if (entry.TypeOfEvent == TimedEventType.LookRandom)
                {
                    entry.Observer.SendMessage("LookRandom");
                }
            }
        }
    }
}
