using DG.Tweening;
using System.Collections;
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

    private Coroutine actionLoop;

    public void StartCombat()
    {
        if (isPowered)
            actionLoop = StartCoroutine(CombatLoop());
    }

    private IEnumerator CombatLoop()
    {
        while (true)
        {
            PerformAction();
            yield return new WaitForSeconds(2f);
        }
    }

    protected virtual void PerformAction()
    {
        // override in subclasses like AttackerCar, RepairCar, etc.
    }

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

    public void AdjustPower(int amount)
    {
        energy += amount;

        if (energy < 0)
        {
            Debug.LogWarning($"Trying to adjust power on car {carName} below 0, setting to 0");
            energy = 0;
        }

        energyDisplay.SetEnergy(energy);
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

    public virtual void OnRearranged()
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
