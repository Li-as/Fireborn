using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPanel : MonoBehaviour
{
    [SerializeField] private Unit[] _units;
    [SerializeField] private UnitView _unitViewTemplate;
    //[SerializeField] private HandCursor _handCursor;

    private List<UnitView> _unitViews = new List<UnitView>();

    private void Start()
    {
        foreach (Unit unit in _units)
        {
            bool isAlreadyExist = false;
            
            foreach (UnitView view in _unitViews)
            {
                if (view.Label.text == unit.Name)
                {
                    //view.Init(unit, _handCursor);
                    view.Init(unit);
                    isAlreadyExist = true;
                }
            }

            if (isAlreadyExist == false)
            {
                UnitView unitView = Instantiate(_unitViewTemplate, transform);
                //unitView.Init(unit, _handCursor);
                unitView.Init(unit);
                _unitViews.Add(unitView);
            }
        }
    }

    //public bool TryEnableUnit(Unit unit)
    //{
    //    foreach (UnitView unitView in _unitViews)
    //    {
    //        if (unitView.gameObject.activeSelf == false)
    //        {
    //            if (unitView.Unit == unit)
    //            {
    //                unitView.gameObject.SetActive(true);
    //                unitView.Appear();
    //                return true;
    //            }
    //        }
    //    }

    //    return false;
    //}
}
