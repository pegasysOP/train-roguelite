using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
   public TrainController playerTrain;
   public TrainController enemyTrain;

    public UnityEvent<bool> OnCombatEnd;

    public void CheckEndCondition()
    {
        if (enemyTrain.AllCarsDestroyed())
            OnCombatEnd.Invoke(true);
        else if (playerTrain.AllCarsDestroyed())
            OnCombatEnd.Invoke(false);
    }
}
