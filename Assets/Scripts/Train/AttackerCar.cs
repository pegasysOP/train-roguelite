using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AttackerCar : TrainCar
{
    protected override void PerformAction()
    {
        TrainCar target = trainController.GetOppositeEnemyCar(this);
        if (target != null)
        {
            target.Damage();
            EncounterHistoryPanel.Instance.AddEntry($"{carName} attacks {target.carName}!");
        }
    }
}
