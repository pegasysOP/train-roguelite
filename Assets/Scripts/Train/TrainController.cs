using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public Transform carContainer;
    public int enginePower = 6; // max number of cars
    public List<TrainCar> cars = new List<TrainCar>();

    public int CarCount => cars.Count;
    public int EmptySlots => enginePower - CarCount;

    public bool CanAddCar => cars.Count < enginePower;

    public void AddCar(TrainCar carPrefab)
    {
        if (!CanAddCar)
        {
            EncounterHistoryPanel.Instance.AddEntry("Not enough engine power to add car!");
            return;
        }

        TrainCar newCar = Instantiate(carPrefab, carContainer);
        cars.Add(newCar);
        RearrangeCars();
    }

    public void RemoveCar(TrainCar car)
    {
        cars.Remove(car);
        Destroy(car.gameObject);
        RearrangeCars();
    }

    private void RearrangeCars()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].transform.localPosition = new Vector3(-i - 1, 0, 0);
        }
    }

    public void SnapCarToNearestSlot(TrainCar draggedCar)
    {
        // TODO: add a min range so that it goes back to the original slot if you drop in a random spot on screen

        cars.Remove(draggedCar);

        int newIndex = 0;
        for (int i = 0; i < cars.Count; i++)
        {
            if (draggedCar.transform.position.x < cars[i].transform.position.x)
                newIndex = i + 1;
        }

        cars.Insert(newIndex, draggedCar);
        RearrangeCars();
    }

    public void ResetTrain()
    {
        foreach (var car in cars)
            Destroy(car.gameObject);

        cars.Clear();
    }
}
