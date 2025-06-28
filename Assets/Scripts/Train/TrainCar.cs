using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class TrainCar : MonoBehaviour
{
    public TrainController trainController;

    public string carName;
    [TextArea(2, 5)] public string carDescription;

    public int currentHealth = 2; // 2: full, 1: damaged, 0: destroyed
    public int energy = 0;

    [Header("Visual")]
    public GameObject damageIndicator;
    public CarEnergyDisplay energyDisplay;

    public bool isDamaged => currentHealth <= 1;
    public bool isDestroyed => currentHealth <= 0;
    public bool isPowered => energy > 0;


    public void Power()
    {
        energy++;
        energyDisplay.AddEnergy();

        OnPowered();
    }

    public void UsePower()
    {
        if (energy > 0)
        {
            energy--;
            energyDisplay.RemoveEnergy();
        }
        else
        {
            Debug.LogWarning($"Trying to use power on car {carName} when it doesn't have enough");
        }
    }

    public virtual void OnHazard()
    {
    }

    public virtual void OnLoot()
    {
    }

    public virtual void OnPowered()
    {
    }

    public virtual void Damage()
    {
        currentHealth--;

        DoPunchAnimation();
        
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

    public void DoPunchAnimation()
    {
        transform.DOPunchScale(Vector3.one / 2, 0.5f, 0, 0);
    }
}
