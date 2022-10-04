using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private Transform _target;
        private EnemyMovement _enemyMovement;
        private EnemyHitpoints _enemyHitpoints;

        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Base").transform;
            
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyHitpoints = GetComponent<EnemyHitpoints>();
        }

        private void Start()
        {
            _enemyMovement.SetEnemyTarget(_target);
        }
    }
}
