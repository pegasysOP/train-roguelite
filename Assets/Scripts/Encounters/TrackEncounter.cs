[System.Serializable]
public class TrackEncounter
{
    public Encounter encounterData;
    public string encounterName => encounterData != null ? encounterData.encounterName : "";
    public string description => encounterData != null ? encounterData.description : "";
}
