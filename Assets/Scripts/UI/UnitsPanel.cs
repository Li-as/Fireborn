using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPanel : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] private UnitView _unitViewTemplate;

    private void Start()
    {
        foreach (Unit unit in _units)
        {
            UnitView unitView = Instantiate(_unitViewTemplate, transform);
            unitView.Init(unit);
        }
    }
}
