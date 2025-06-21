using UnityEngine;

public class SpikeCar : TrainCar
{
    [Header("Spike")]
    public int attackDamage = 2;

    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to attack");
    }
}
