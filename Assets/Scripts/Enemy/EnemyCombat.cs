using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackRange = 3;
    [SerializeField] private float _attackSpeed = 1;

    private Transform _target;
    private float _currentTime;

    private void Update()
    {
        AttackTarget();
    }

    private void AttackTarget()
    {
        if (_target == null) return;

        if (Vector3.Distance(_target.position,transform.position) < _attackRange)
        {
            if (_currentTime < Time.time)
            {
                _currentTime = Time.time + _attackSpeed;

                _target.GetComponent<IDamageable>()?.TakeDamage(_attackDamage);
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
