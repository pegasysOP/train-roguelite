using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Testing")]
    public TrainController trainController;
    public ZoneManager zoneManager;
    public List<TrainCar> testCarPrefabs = new List<TrainCar>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    { 
        // for testing

        foreach (TrainCar car in testCarPrefabs)
            trainController.AddCar(car);

        zoneManager.upcomingEncounters = new List<TrackEncounter>
        {
            new TrackEncounter() { type = EncounterType.Hazard, description = "Rockslide" },
            new TrackEncounter() { type = EncounterType.Loot, description = "Scrap pile" },
            new TrackEncounter() { type = EncounterType.Hazard, description = "Bandit fire" }
        };
    }
}
