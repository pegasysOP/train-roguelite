public enum EncounterType
{
    Hazard,
    Loot,
    Fork
}

[System.Serializable]
public class TrackEncounter
{
    public EncounterType type;
    public string description;
}
