using UnityEngine;

[CreateAssetMenu(menuName = "Encounters/Hazard")]
public class HazardEncounter : Encounter
{
    public override void Resolve(TrainController train)
    {
        foreach (TrainCar car in train.cars)
        {
            if (car is ShieldCar shieldCar && !car.isDamaged)
            {
                EncounterHistoryPanel.Instance.AddEntry("Shield car blocked hazard!");
                car.DoPunchAnimation();
                return;
            }
        }

        TrainCar target = train.cars[Random.Range(0, train.CarCount)];
        target.Damage();
        EncounterHistoryPanel.Instance.AddEntry($"Hazard hits {target.carName}");
    }
}
