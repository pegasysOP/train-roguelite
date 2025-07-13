using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public Transform carContainer;
    public int enginePower = 6; // max number of cars
    public List<TrainCar> cars = new List<TrainCar>();

    [Header("Config")]
    public bool AllowRearrangement = false;
    public bool isEnemy = false;

    [Header("TESTING")]
    public List<TrainCar> testCarPrefabs = new List<TrainCar>();

    public int CarCount => cars.Count;
    public int EmptySlots => enginePower - CarCount;
    public bool CanAddCar => cars.Count < enginePower;

    private CombatManager combatManager;

    private void Start()
    {
        // for testing
        foreach (TrainCar car in testCarPrefabs)
            AddCar(car);

        PowerCars();
    }

    public void StartCombat(CombatManager combatManager)
    {
        this.combatManager = combatManager;

        AllowRearrangement = false;
    }

    public void EndCombat()
    {
        this.combatManager = null;

        AllowRearrangement = !isEnemy; // only allow for player
    }

    public bool AllCarsDestroyed()
    {
        foreach (TrainCar car in cars)
        {
            if (!car.isDestroyed)
                return false;
        }
        return true;
    }

    public TrainCar GetOppositeEnemyCar(TrainCar attackingCar)
    {
        int index = GetCarIndex(attackingCar);
        if (index < 0 || index >= cars.Count)
            return null;

        // if over the end just target last car
        if (index >= combatManager.enemyTrain.CarCount)
            index = combatManager.enemyTrain.CarCount - 1;

        return combatManager.enemyTrain.cars[index];
    }

    public void AddCar(TrainCar carPrefab, bool animate = false)
    {
        if (!CanAddCar)
        {
            EncounterHistoryPanel.Instance.AddEntry("Not enough engine power to add car!");
            TextBox.Instance.ShowText("Not enough engine power to add car!");
            return;
        }

        TrainCar newCar = Instantiate(carPrefab, carContainer);
        newCar.trainController = this;
        cars.Add(newCar);
        RearrangeCars(animate);
    }

    public void RemoveCar(TrainCar car, bool animate = false)
    {
        cars.Remove(car);
        Destroy(car.gameObject);
        RearrangeCars(animate);
    }

    public void PowerCars()
    {
        foreach (TrainCar car in cars)
        {
            car.Power();
        }
    }

    private void RearrangeCars(bool animate)
    {
        if (animate)
        {
            for (int i = 0; i < cars.Count; i++)
                StartCoroutine(MoveToPosition(cars[i].transform, GetCarSlotPosition(i)));
        }
        else
        {
            for (int i = 0; i < cars.Count; i++)
                cars[i].transform.localPosition = GetCarSlotPosition(i);
        }
    }

    public Vector3 GetCarSlotPosition(int index)
    {
        return new Vector3(-index - 1, 0, 0f);
    }

    private IEnumerator MoveToPosition(Transform movingObject, Vector3 targetPos)
    {
        float t = 0;
        Vector3 startPos = movingObject.localPosition;

        while (t < 1f)
        {
            t += Time.deltaTime * 6f;
            movingObject.localPosition = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        movingObject.localPosition = targetPos;
    }

    public void SnapCarToNearestSlot(TrainCar draggedCar)
    {
        int closestIndex = cars.IndexOf(draggedCar);
        float shortestDistance = Mathf.Infinity;

        for (int i = 0; i < CarCount; i++)
        {
            float carSlotPos = GetCarSlotPosition(i).x;

            float distance = Mathf.Abs(draggedCar.transform.localPosition.x - GetCarSlotPosition(i).x);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestIndex = i;
            }
        }

        cars.Remove(draggedCar);
        cars.Insert(closestIndex, draggedCar);

        foreach (TrainCar car in cars)
            car.OnRearranged();

        RearrangeCars(true);
    }

    public void ResetTrain()
    {
        foreach (var car in cars)
            Destroy(car.gameObject);

        cars.Clear();
    }

    #region Car Helpers

    public int GetCarIndex(TrainCar car)
    {
        return cars.IndexOf(car);
    }

    public TrainCar GetLeftNeighbor(TrainCar car)
    {
        int index = GetCarIndex(car);
        return (index < cars.Count - 1) ? cars[index + 1] : null;
    }

    public TrainCar GetRightNeighbor(TrainCar car)
    {
        int index = GetCarIndex(car);
        return (index > 0) ? cars[index - 1] : null;
    }


    #endregion
}
