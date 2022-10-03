using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewStructure : Structure
{
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;

    public bool CanPlace { get; private set; }

    private void Start()
    {
        CanPlace = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CanPlace = false;
        ChangeMaterial(CanPlace);
    }

    private void OnTriggerExit(Collider other)
    {
        CanPlace = true;
        ChangeMaterial(CanPlace);
    }

    private void ChangeMaterial(bool canPlace) 
    {
        var renderers = GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            renderer.material = canPlace ? _greenMaterial : _redMaterial;
        }
    }
}
