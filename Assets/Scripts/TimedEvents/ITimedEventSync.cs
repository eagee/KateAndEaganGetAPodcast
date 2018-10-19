using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the keyframe index of this object to the nearest possible value 
/// to the specified time offset.
/// </summary>
public interface ITimedEventSync {
    void MoveToTimeOffset(float timeOffset);
}

/// <summary>
/// Provides a container implementation that can be used with the Unity Editor to provide a list of
/// polymorphic ITimedEventSync implementations.
/// </summary>
[Serializable]
public class ITimedEventSyncContainer : IUnifiedContainer<ITimedEventSync> {
}
