using UnityEngine;

[CreateAssetMenu(menuName = "Encounters/Loot")]
public class LootEncounter : Encounter
{
    public override void Resolve(TrainController train)
    {
        foreach (TrainCar car in train.cars)
        {
            if (car is CollectorCar collectorCar && !car.isDamaged && car.isPowered)
            {
                EncounterHistoryPanel.Instance.AddEntry($"Extra {collectorCar.collectionQuantity} scrap collected!");
                GameManager.Instance.AddScrap(1 + collectorCar.collectionQuantity);
                car.UsePower();
                car.DoPunchAnimation();
                return;
            }
        }

        GameManager.Instance.AddScrap(1); // only 1 by default
        EncounterHistoryPanel.Instance.AddEntry("Missed chance at extra loot");
    }
}
