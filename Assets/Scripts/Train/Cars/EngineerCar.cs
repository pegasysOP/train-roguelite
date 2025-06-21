using UnityEngine;

public class EngineerCar : TrainCar
{
    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to repair");
    }
}
