using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerConstruction : MonoBehaviour
{
    [SerializeField] private List<Structure> _structures = new List<Structure>();
    [SerializeField] private List<PreviewStructure> _previewStructures = new List<PreviewStructure>();
    [SerializeField] private int _structureIndex = 0;
    [SerializeField] private LayerMask _buildingMask;

    private Vector3 _previewPosition;
    private Vector3 _previewRotation;

    private void Update()
    {
        HandlePreview();
        HandleConstruction();
    }

    private void HandlePreview()
    {
        if (NavMesh.SamplePosition(MousePosition(), out NavMeshHit navHit, 0.1f, NavMesh.AllAreas))
            _previewPosition = navHit.position;

        _previewRotation = new Vector3(0, 
                        Mathf.Repeat(_previewRotation.y + Input.mouseScrollDelta.normalized.y * 10,360), 0);

        for (int i = 0; i < _previewStructures.Count; i++)
        {
            _previewStructures[i].transform.position = _previewPosition;
            _previewStructures[i].transform.eulerAngles = _previewRotation;
        }
    }

    private void HandleConstruction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (NavMesh.SamplePosition(MousePosition(), out NavMeshHit navHit, 0.1f, NavMesh.AllAreas))
            {
                if (_previewStructures[_structureIndex].CanPlace == false) return;
                
                Instantiate(_structures[_structureIndex], _previewPosition, Quaternion.Euler(_previewRotation));
            }
        }
    }

    private Vector3 MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _buildingMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
