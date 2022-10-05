using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [field: SerializeField] public int StartCredits { private get; set; }
    public int Credits { get; private set; }

    private void Start()
    {
        Credits = StartCredits;
        UpdateUI();
    }

    public void AddCredits(int amount)
    {
        Credits += amount;
        UpdateUI();
    }

    public bool SubtractCredits(int amount)
    {
        if (Credits >= amount)
        {
            Credits -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI() 
    {
        UIManager.Instance.SetResources(Credits);
    }
}
