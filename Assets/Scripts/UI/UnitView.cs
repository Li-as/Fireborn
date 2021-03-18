using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _waterPower;
    [SerializeField] private int _dragAreaDistanceFromCamera;

    private Unit _unit;

    public void Init(Unit unit)
    {
        _unit = unit;
        _icon.sprite = unit.Sprite;
        _label.text = unit.Name;
        _waterPower.text = unit.WaterPowerLevel.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = 0;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //_unit.transform.position = mousePosition;

        _unit.gameObject.SetActive(true);
        _unit.Rigidbody.isKinematic = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = _dragAreaDistanceFromCamera;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //_unit.transform.position = mousePosition;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _dragAreaDistanceFromCamera;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Vector3 direction = new Vector3(0, -1, 1).normalized;
        Vector3 direction = new Vector3(0, -1, 12f / 6f).normalized;
        Ray ray = new Ray(mousePosition, direction);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            if (hitInfo.transform.TryGetComponent(out UnitsDragArea dragArea))
            {
                _unit.transform.position = hitInfo.point;
            }
        }

        //Debug.Log($"Now Unit is at {_unit.transform.position}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag stopped");

        _unit.Rigidbody.isKinematic = false;
    }
}
