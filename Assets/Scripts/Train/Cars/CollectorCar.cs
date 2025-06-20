using UnityEngine;

public class CollectorCar : TrainCar
{
    [Header("Collector")]
    public int collectionQuantity = 3;

    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to collect");
    }
}
