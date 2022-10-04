using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _rotationSpeed = 5;

    private void Awake()
    {
        _agent.updateRotation = false;
        _agent.isStopped = true;
    }

    private void Update()
    {
        RotateEnemy();
        CheckPathStatus();
    }

    public void SetEnemyTarget(Transform target)
    {
        _agent.SetDestination(target.position);
        _agent.isStopped = false;
    }

    private void RotateEnemy()
    {
        if (_agent.isStopped == false)
        {
            var dir = _agent.steeringTarget - transform.position;
            dir.y = 0;
            var newDir = Vector3.RotateTowards(transform.forward, dir, _rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    private void CheckPathStatus()
    {
        if(_agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            if (Vector3.Distance(transform.position, _agent.pathEndPosition) < _agent.stoppingDistance)
            {
                _agent.path.ClearCorners();
                _agent.isStopped = true;
            }
        }
    }
}
