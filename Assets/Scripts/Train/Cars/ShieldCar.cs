using UnityEngine;

public class ShieldCar : TrainCar
{
    public override void OnPowered()
    {
        Debug.Log($"{carName} is powered and ready to block damage");
    }
}
