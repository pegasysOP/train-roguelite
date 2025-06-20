using System.Collections;
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
            //cars[i].transform.localPosition = new Vector3(-i - 1, 0, 0);
            StartCoroutine(MoveToPosition(cars[i].transform, GetCarSlotPosition(i)));
        }
    }

    public Vector3 GetCarSlotPosition(int index)
    {
        return new Vector3(-index - 1, 0f, 0f);
    }

    private IEnumerator MoveToPosition(Transform movingObject, Vector3 targetPos)
    {
        float t = 0;
        Vector3 startPos = movingObject.position;

        while (t < 1f)
        {
            t += Time.deltaTime * 6f;
            movingObject.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        movingObject.position = targetPos;
    }

    public void SnapCarToNearestSlot(TrainCar draggedCar)
    {
        int closestIndex = cars.IndexOf(draggedCar);
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < CarCount; i++)
        {
            float carSlotPos = GetCarSlotPosition(i).x;

            float distance = Mathf.Abs(draggedCar.transform.position.x - GetCarSlotPosition(i).x);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestIndex = i;
            }
        }

        cars.Remove(draggedCar);
        cars.Insert(closestIndex, draggedCar);
        RearrangeCars();
    }

    public void ResetTrain()
    {
        foreach (var car in cars)
            Destroy(car.gameObject);

        cars.Clear();
    }
}
