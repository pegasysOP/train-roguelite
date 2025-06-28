using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AdjacentCar : TrainCar
{
    private TrainCar previousLeft;
    private TrainCar previousRight;

    public override void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered and ready to boost");
    }

    public override void OnRearranged()
    {
        TrainCar currentLeft = trainController.GetLeftNeighbor(this);
        TrainCar currentRight = trainController.GetRightNeighbor(this);

        // remove energy from previous
        if (previousLeft != null)
            previousLeft.AdjustPower(-1);
        if (previousRight != null)
            previousRight.AdjustPower(-1);

        // add energy to new
        if (currentLeft != null)
            currentLeft.AdjustPower(1);
        if (currentRight != null)
            currentRight.AdjustPower(1);

        previousLeft = currentLeft;
        previousRight = currentRight;
    }
}
