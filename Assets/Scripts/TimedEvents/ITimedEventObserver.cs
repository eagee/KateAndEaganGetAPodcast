using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimedEventObserver
{
    void LookAtObject(GameObject targetObject);
    void LookRandom();
}
