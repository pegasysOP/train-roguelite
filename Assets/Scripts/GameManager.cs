using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Testing")]
    public TrainController trainController;
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
        trainController.AddCar(testCarPrefabs[0]);
        trainController.AddCar(testCarPrefabs[1]);
        trainController.AddCar(testCarPrefabs[2]);
    }
}
