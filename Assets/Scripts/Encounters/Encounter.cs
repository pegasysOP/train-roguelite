using UnityEngine;

public abstract class Encounter : ScriptableObject
{
    public string encounterName;
    [TextArea(2, 5)]
    public string description;

    public abstract void Resolve(TrainController train);
}
