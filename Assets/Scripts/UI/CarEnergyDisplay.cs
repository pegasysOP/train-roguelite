using System.Collections.Generic;
using UnityEngine;

public class CarEnergyDisplay : MonoBehaviour
{
    public GameObject energyBarPrefab;

    private List<GameObject> energyBars = new List<GameObject>();

    public void AddEnergy(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bar = Instantiate(energyBarPrefab, transform);
            energyBars.Add(bar);
        }
    }

    public void RemoveEnergy(int amount = 1)
    {
        if (energyBars.Count < amount)
            Debug.LogWarning("Trying to remove more energy than is remaining");

        for (int i = 0; i < amount; i++)
        {
            if (i >= energyBars.Count)
                break;

            int index = energyBars.Count - i - 1;

            GameObject bar = energyBars[index];
            energyBars.RemoveAt(index);
            Destroy(bar);
        }
    }

    public void SetEnergy(int amount)
    {
        ClearEnergy();

        AddEnergy(amount);
    }

    public void ClearEnergy()
    {
        foreach (GameObject bar in energyBars)
            Destroy(bar);

        energyBars.Clear();
    }
}
