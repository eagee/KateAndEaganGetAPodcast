using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyMinnow.SALSA;

[System.Serializable]
public class TimedFaceEventObserver : TimedEventObserver
{
    public EmotionProperties BemusedProperties;
    public EmotionProperties NeutralProperties;
    public EmotionProperties HappyProperties;
    private Animator EyeLeft;
    private Animator EyeRight;
    private Salsa2D Mouth;

    private void Start()
    {
        // Populate our eye renderes using child objects (so we don't have to drag them in via the editors)
        var animators = GetComponentsInChildren<Animator>();
        foreach(var animator in animators)
        {
            if (animator.gameObject.name == "EyeLeft")
                EyeLeft = animator;
            if (animator.gameObject.name == "EyeRight")
                EyeRight = animator;
        }

        Mouth = GetComponentInChildren<Salsa2D>();
    }

    // Method called when TimedEventManager sends LookAtObject message
    new public void LookAtObject(GameObject targetObject)
    {
        GetComponent<RandomEyes2D>().SetLookTarget(targetObject);
    }

    // Method called when TimedEventMangager sends LookRandom message
    new public void LookRandom()
    {
        GetComponent<RandomEyes2D>().SetLookTarget(null);
    }

    new public void EmoteNormal()
    {
        if(EyeLeft)
        {
            EyeLeft.SetBool("Amused", false);
        }
        if(EyeRight)
        {
            EyeRight.SetBool("Amused", false);
        }
        if(Mouth)
        {
            Mouth.sayRestSprite = HappyProperties.Resting;
            Mouth.saySmallSprite = HappyProperties.Small;
            Mouth.sayMediumSprite = HappyProperties.Medium;
            Mouth.sayLargeSprite = HappyProperties.Large;
        }
    }

    new public void EmoteAmusement()
    {
        if (EyeLeft)
        {
            EyeLeft.SetBool("Amused", true);
        }
        if (EyeRight)
        {
            EyeRight.SetBool("Amused", true);
        }
        if (Mouth)
        {
            Mouth.sayRestSprite = HappyProperties.Resting;
            Mouth.saySmallSprite = HappyProperties.Small;
            Mouth.sayMediumSprite = HappyProperties.Medium;
            Mouth.sayLargeSprite = HappyProperties.Large;
        }
    }

    new public void EmoteNonplussed()
    {
        if (EyeLeft)
        {
            EyeLeft.SetBool("Amused", false);
        }
        if (EyeRight)
        {
            EyeRight.SetBool("Amused", false);
        }
        if (Mouth)
        {
            Mouth.sayRestSprite = NeutralProperties.Resting;
            Mouth.saySmallSprite = NeutralProperties.Small;
            Mouth.sayMediumSprite = NeutralProperties.Medium;
            Mouth.sayLargeSprite = NeutralProperties.Large;
        }
    }

    new public void EmoteBemused()
    {
        if (EyeLeft)
        {
            EyeLeft.SetBool("Amused", false);
        }
        if (EyeRight)
        {
            EyeRight.SetBool("Amused", false);
        }
        if (Mouth)
        {
            Mouth.sayRestSprite = BemusedProperties.Resting;
            Mouth.saySmallSprite = BemusedProperties.Small;
            Mouth.sayMediumSprite = BemusedProperties.Medium;
            Mouth.sayLargeSprite = BemusedProperties.Large;
        }
    }

    new public void EmotePregnantBlink()
    {
        GetComponent<RandomEyes2D>().Blink(0.25f);
    }

};
