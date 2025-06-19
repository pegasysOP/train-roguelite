using UnityEngine;

public class TrainCar : MonoBehaviour
{
    public string carName;
    public int currentHealth = 2; // 2: full, 1: damaged, 0: destroyed

    public bool isBroken => currentHealth <= 0;

    public virtual void OnHazard() { }
    public virtual void OnLoot() { }
    public virtual void OnPowered() { }
}
