using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Structures
{
    public class Turret : Structure
    {
        [SerializeField] private LayerMask _enemiesMask;
        [SerializeField] private LayerMask _wallMask;
        [SerializeField] private float _attackRange = 10;
        [SerializeField] private float _attackSpeed = 1;
        [SerializeField] private int _attackDamage = 10;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _attackAngle = 90;
        [SerializeField] private float _attackDivider = 2;
        [SerializeField] private Transform _raycastSource;
        [SerializeField] private Transform _turretHousing;
        [SerializeField] private Transform[] _barrels;

        private Transform _target;
        private float _currentTime;
        private int _barrelIndex = 0;

        private void Update()
        {
            RotateTurret();
            AttackEnemy();
        }

        private void RotateTurret()
        {
            _target = GetClosestEnemy();
            if (_target == null) return;

            var dir = _target.transform.position - _turretHousing.position;
            dir.y = 0;
            var newDir = Vector3.RotateTowards(_turretHousing.forward, dir, _rotationSpeed * Time.deltaTime, 0.0f);
            _turretHousing.rotation = Quaternion.LookRotation(newDir);
        }

        private void AttackEnemy()
        {
            if (_target == null) return;

            var dir = _target.position - _raycastSource.position;
            dir.y = 0;
            var angle = Vector3.Angle(dir, _turretHousing.forward);

            if (angle >= -_attackAngle/_attackDivider && angle <= _attackAngle/_attackDivider)
            {
                if (_currentTime < Time.time)
                {
                    _currentTime = Time.time + _attackSpeed;
                    _barrelIndex = (int)Mathf.Repeat(_barrelIndex += 1, _barrels.Length);

                    if (Physics.Raycast(_barrels[_barrelIndex].position, _barrels[_barrelIndex].forward, out RaycastHit hit,_attackRange,_enemiesMask))
                    {
                        hit.transform.GetComponent<IDamageable>()?.TakeDamage(_attackDamage);
                        Debug.DrawRay(_barrels[_barrelIndex].position, _barrels[_barrelIndex].forward * _attackDamage, Color.red, 0.25f);
                    }
                }   
            }
        }

        private Transform GetClosestEnemy()
        {
            var enemies = Physics.OverlapSphere(_turretHousing.position, _attackRange, _enemiesMask);

            Transform closestEnemy = null;
            float minDist = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                var dist = Vector3.Distance(_turretHousing.position, enemies[i].transform.position);

                if (dist < minDist)
                {
                    var dir = enemies[i].transform.position - _raycastSource.position;

                    if (Physics.Raycast(_raycastSource.position, dir, out RaycastHit hit, _attackRange, _wallMask))
                    {
                        if (hit.transform.CompareTag("Enemy"))
                        {
                            closestEnemy = enemies[i].transform;
                            minDist = dist;
                        }
                    }
                }
            }

            return closestEnemy;
        }
    }
}
