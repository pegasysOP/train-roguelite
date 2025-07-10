using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    public TrainController playerTrain;
    public TrainController enemyTrain;

    [Header("Settings")]
    public float combatDuration = 30f;

    private float combatTimer;
    private bool combatRunning = false;

    public void StartCombat()
    {
        if (combatRunning)
            return;

        combatRunning = true;
        combatTimer = combatDuration;

        playerTrain.StartCombat(this);
        enemyTrain.StartCombat(this);

        EncounterHistoryPanel.Instance.AddEntry("Combat started");

        StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (combatRunning)
        {
            combatTimer -= Time.deltaTime;

            // player loss
            if (playerTrain.AllCarsDestroyed())
            {
                EndCombat(false);
                yield break;
            }

            // enemy defeated
            if (enemyTrain.AllCarsDestroyed())
            {
                EndCombat(true);
                yield break;
            }

            // survived till end
            if (combatTimer <= 0f)
            {
                EndCombat(true);
                yield break;
            }

            yield return null;
        }
    }

    private void EndCombat(bool playerWon)
    {
        combatRunning = false;

        playerTrain.EndCombat();
        enemyTrain.EndCombat();

        if (playerWon)
        {
            EncounterHistoryPanel.Instance.AddEntry("You won the combat!");
        }
        else
        {
            EncounterHistoryPanel.Instance.AddEntry("You were defeated...");
        }
    }

    public bool IsCombatRunning()
    {
        return combatRunning;
    }
}
