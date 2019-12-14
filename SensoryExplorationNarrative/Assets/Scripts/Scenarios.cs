using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Scenario
{
    public string mainText;
    public Interaction[] scenarioInteractions;

    public Scenario(string text, Interaction[] interactions)
    {  
        mainText = text;
        scenarioInteractions = interactions;
    }
}

public struct Interaction
{
    public InteractionTypes interactionType; //used to determine the button sprite.

    public string interactionDescription;

    public int outcome; //move to scenarioID

    public Interaction(InteractionTypes type, string description, int outc)
    {
        interactionType = type;
        interactionDescription = description;
        outcome = outc;
    }

}

public enum InteractionTypes
{
    none,
    touch,
    listen,
    look,
    smell,
    first,
    second
}
