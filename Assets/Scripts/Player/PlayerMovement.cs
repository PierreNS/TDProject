using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] private LayerMask _rotationMask;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        HandleMove();
        HandleRotation();
    }

    private void HandleMove()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var Vertical = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(horizontal, 0, Vertical);

        _characterController.Move(_moveDirection * _moveSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,_rotationMask))
        {
            var dir = (hit.point - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(dir.x,0,dir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
