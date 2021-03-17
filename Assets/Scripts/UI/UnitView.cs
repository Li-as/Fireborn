using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _waterPower;

    public void Init(Unit unit)
    {
        _icon.sprite = unit.Sprite;
        _label.text = unit.Name;
        _waterPower.text = unit.WaterPowerLevel.ToString();
    }
}
