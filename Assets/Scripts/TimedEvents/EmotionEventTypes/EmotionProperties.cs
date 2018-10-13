using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmotionProperties
{
    public Sprite Resting;
    public Sprite Small;
    public Sprite Medium;
    public Sprite Large;

    public EmotionProperties(Sprite Resting, Sprite Small, Sprite Medium, Sprite Large)
    {
        this.Resting = Resting;
        this.Small = Small;
        this.Medium = Medium;
        this.Large = Large;
    }
}
