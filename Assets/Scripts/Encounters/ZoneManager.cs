using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public TrainController trainController;
    public int energyPerZone = 3;
    public List<Encounter> upcomingEncounters = new List<Encounter>();

    private int energy;


    public void StartNextEncounter()
    {
        if (upcomingEncounters.Count == 0)
        {
            EncounterHistoryPanel.Instance.AddEntry($"End of <b>Zone</b>");
            TextBox.Instance.ShowText($"End of <b>Zone</b>");
            Debug.LogWarning("No upcoming encounters to resolve");
            return;
        }

        Encounter currentEncounter = upcomingEncounters[0];
        upcomingEncounters.RemoveAt(0);

        EncounterHistoryPanel.Instance.AddEntry($"<b>Encounter</b>: {currentEncounter.encounterName} - {currentEncounter.description}");
        TextBox.Instance.ShowText($"<b>Encounter</b>: {currentEncounter.encounterName} - {currentEncounter.description}");
        currentEncounter.Resolve(trainController);
    }

    public void AssignPowerToCar(TrainCar car)
    {
        if (energy <= 0 || car == null)
            return;

        car.OnPowered();
        energy--;
    }
}
