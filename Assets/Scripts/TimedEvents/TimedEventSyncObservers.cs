using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumed by TimedEventMangaer to provide a list of ITimedEventSync objects that will syncrhonize
/// their timers to the start offset value provided by TimedEventManager.
/// </summary>
public class TimedEventSyncObservers : MonoBehaviour {
    public List<ITimedEventSyncContainer> TimedEventSyncObjects;
}
