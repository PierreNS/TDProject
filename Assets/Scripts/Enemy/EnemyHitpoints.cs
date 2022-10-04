using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitpoints : MonoBehaviour, IDamageable
{
    public event Action DieEvent;
    [SerializeField] private int _hitPoints;

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;

        if (_hitPoints <= 0)
        {
            DieEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
