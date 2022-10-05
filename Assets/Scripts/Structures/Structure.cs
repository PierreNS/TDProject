using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IDamageable
{
    public event Action DestructionEvent;

    [field: SerializeField] public int Cost { get; set; }
    [field: SerializeField] public int HitPoints { get; set; }
    [field: SerializeField] public int MaxHitPoints { get; set; }

    void Start()
    {
        HitPoints = MaxHitPoints;
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;

        var lerpVal = Mathf.InverseLerp(0, MaxHitPoints, HitPoints);
        UIManager.Instance.SetBaseHitpoints(lerpVal);

        if (HitPoints <= 0)
        {
            DestructionEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
