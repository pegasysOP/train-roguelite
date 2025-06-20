using UnityEngine;

public class TrainCar : MonoBehaviour
{
    public string carName;
    public int currentHealth = 2; // 2: full, 1: damaged, 0: destroyed

    [Header("Visual")]
    public GameObject damageIndicator;

    public bool isDamaged => currentHealth <= 1;
    public bool isDestroyed => currentHealth <= 0;

    public virtual void OnHazard()
    {
    }

    public virtual void OnLoot()
    {
    }

    public virtual void OnPowered()
    {
        EncounterHistoryPanel.Instance.AddEntry($"{carName} is powered");
    }

    public virtual void Damage()
    {
        currentHealth--;
        
        if (isDestroyed)
        {
            EncounterHistoryPanel.Instance.AddEntry($"{carName} is destroyed!");
            gameObject.SetActive(false);
        }
        else if (isDamaged)
        {
            EncounterHistoryPanel.Instance.AddEntry($"{carName} is damaged!");
            damageIndicator.SetActive(true);
        }
    }
}
