using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Scenario
{
    public int scenarioID;
    public string mainText;
    public Interaction[] scenarioInteractions;

    public Scenario(int ID, string text, Interaction[] interactions)
    {
        scenarioID = ID;
        mainText = text;
        scenarioInteractions = interactions;
    }
}

public struct Interaction
{
    public interactionTypes interactionType; //used to determine the button sprite.

    public string interactionDescription;

    public int outcome; //move to scenarioID

    public Interaction(interactionTypes type, string description, int outc)
    {
        interactionType = type;
        interactionDescription = description;
        outcome = outc;
    }

}

public enum interactionTypes
{
    none,
    touch,
    listen,
    look,
    smell,
    first,
    second
}
