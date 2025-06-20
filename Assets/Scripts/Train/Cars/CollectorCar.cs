using UnityEngine;

public class CollectorCar : TrainCar
{
    public int collectionQuantity = 3;

    public override void OnPowered()
    {
        Debug.Log($"{carName} is powered and ready to collect");
    }
}
