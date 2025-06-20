using UnityEngine;

public class ShieldCar : TrainCar
{
    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to block damage");
    }
}
