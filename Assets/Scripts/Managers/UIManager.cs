using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text _resourcesTxt;
    [SerializeField] private TMP_Text _enemiesTxt;
    [SerializeField] private Slider _hitpointSlider;
    [SerializeField] private Image[] _structureImages;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void SetResources(int value)
    {
        _resourcesTxt.text = $"Resources: {value}";
    }

    public void SetEnemyAmount(int value)
    {
        _enemiesTxt.text = $"Enemies: {value}";
    }

    public void SetBaseHitpoints(float value)
    {
        _hitpointSlider.value = value;
    }
    
    public void ResetSelectedStructure() 
    {
        for (int i = 0; i < _structureImages.Length; i++)
        {
            _structureImages[i].color = Color.white;
        }
    }

    public void ChangeSelectedStructure(int index) 
    {
        ResetSelectedStructure();
        _structureImages[index].color = Color.green;
    }
}
