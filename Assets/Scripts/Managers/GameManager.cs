using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Structure _base;

    private void Start()
    {
        _base.DestructionEvent += OnDestructionEvent;
    }

    private void OnDestructionEvent()
    {
        SpawnManager.instance.DisableSpawning();
    }
}
