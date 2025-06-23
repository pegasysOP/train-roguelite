using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TrainController trainController;
    public ZoneManager zoneManager;
    public List<TrainCar> testCarPrefabs = new List<TrainCar>();

    [Header("UI")]
    public TextMeshProUGUI scrapCounter;

    private int scrapCount = 0;

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
    }

    public void AddScrap(int amount)
    {
        scrapCount += amount;
        scrapCounter.text = scrapCount.ToString();
    }
}
