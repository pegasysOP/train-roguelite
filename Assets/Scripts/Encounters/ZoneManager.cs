using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public TrainController trainController;
    public int energyPerZone = 3;
    public List<TrackEncounter> upcomingEncounters= new List<TrackEncounter>();

    private int energy;
    private int encounterIndex = 0;

    public void StartZone()
    {
        energy = energyPerZone;
        encounterIndex = 0;
        ProceedToNextEncounter();
    }

    public void AssignPowerToCar(TrainCar car)
    {
        if (energy <= 0 || car == null)
            return;

        car.OnPowered();
        energy--;
    }

    private void ProceedToNextEncounter()
    {
        if (encounterIndex >= upcomingEncounters.Count)
        {
            EncounterHistoryPanel.Instance.AddEntry("Zone complete!");
            return;
        }

        TrackEncounter encounter = upcomingEncounters[encounterIndex];
        EncounterHistoryPanel.Instance.AddEntry(encounter);

        ResolveEncounter(encounter);
        encounterIndex++;

        // wait and call next encounter after a delay for now
        Invoke(nameof(ProceedToNextEncounter), 2f);
    }

    private void ResolveEncounter(TrackEncounter encounter)
    {
        switch (encounter.type)
        {
            case EncounterType.Hazard:
                ResolveHazard();
                break;
            case EncounterType.Loot:
                ResolveLoot();
                break;
            case EncounterType.Fork:
                Debug.Log("Fork not yet implemented");
                break;
        }
    }

    private void ResolveHazard()
    {
        //  shield car blocks damage
        foreach (TrainCar car in trainController.cars)
        {
            if (car is ShieldCar shieldCar && !car.isDamaged)
            {
                EncounterHistoryPanel.Instance.AddEntry("Shield blocked hazard!");
                return;
            }
        }

        // attack random car for now
        EncounterHistoryPanel.Instance.AddEntry("Hazard hits!");
        TrainCar attackedCar = trainController.cars[Random.Range(0, trainController.CarCount)];        
        attackedCar.currentHealth--;

        if (attackedCar.isBroken)
            EncounterHistoryPanel.Instance.AddEntry($"{attackedCar.name} is broken!");
    }

    private void ResolveLoot()
    {
        foreach (TrainCar car in trainController.cars)
        {
            if (car is CollectorCar collectorCar && !car.isDamaged)
            {
                EncounterHistoryPanel.Instance.AddEntry("Loot collected!");
                
                // add extra scrap here

                return;
            }
        }

        EncounterHistoryPanel.Instance.AddEntry("Missed loot!");
    }
}
