using UnityEngine;

public class AdjacentCar : TrainCar
{
    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to boost");
    }
}
