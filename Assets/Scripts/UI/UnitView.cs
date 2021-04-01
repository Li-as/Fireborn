using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class UnitView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _waterPower;
    [SerializeField] private TMP_Text _unitsAmountDisplay;
    [SerializeField] private int _dragAreaDistanceFromCamera;
    [SerializeField] private float _appearDisappearDelay;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private string _appearAnimationTrigger;
    [SerializeField] private float _changeAmountDelay;
    [SerializeField] private string _amountDisplayIncreaseAnimationTrigger;
    [SerializeField] private string _amountDisplayDecreaseAnimationTrigger;
    //[SerializeField] private Texture2D _cursorHand;
    //[SerializeField] private Vector2 _cursorHandHotSpot;
    //[SerializeField] private Texture2D _cursorHandTap;
    //[SerializeField] private Vector2 _cursorHandTapHotSpot;
    //[SerializeField] private Texture2D[] _disappearingCursorSteps;
    //[SerializeField] private float _cursorDisappearTime;

    private Animator _animator;
    private List<Unit> _units = new List<Unit>();
    private GameObject _unitIcon;
    private int _unitsAmount;
    private PlaceOnFire _placeUnderUnit;
    //private HandCursor _handCursor;

    public TMP_Text Label => _label;

    //public void Init(Unit unit, HandCursor cursor)
    public void Init(Unit unit)
    {
        if (_units.Count == 0)
        {
            _animator = GetComponent<Animator>();

            //_icon.sprite = unit.Sprite;
            _label.text = unit.Name;
            _waterPower.text = unit.WaterPowerLevel.ToString();
            if (unit.Icon != null)
            {
                _unitIcon = Instantiate(unit.Icon, transform);
            }
        }

        _units.Add(unit);
        _unitsAmount++;
        _unitsAmountDisplay.text = _unitsAmount.ToString();

        //_handCursor = cursor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = 0;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //_unit.transform.position = mousePosition;

        _units[0].gameObject.SetActive(true);
        _units[0].Collider.enabled = false;
        _units[0].Rigidbody.isKinematic = true;
        _units[0].Rigidbody.useGravity = true;

        //Vector3 cursorTargetPosition = Input.mousePosition;
        //cursorTargetPosition.z = 5;
        //cursorTargetPosition = Camera.main.ScreenToWorldPoint(cursorTargetPosition);
        //_handCursor.transform.position = cursorTargetPosition;
        //Color imageColor = _handCursor.Image.color;
        //imageColor.a = 1;
        //_handCursor.Image.color = imageColor;
        //_handCursor.transform.rotation = Quaternion.Euler(0, 0, 50);

        //Cursor.visible = true;
        //Cursor.SetCursor(_cursorHandTap, _cursorHandTapHotSpot, CursorMode.Auto);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = _dragAreaDistanceFromCamera;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //_unit.transform.position = mousePosition;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _dragAreaDistanceFromCamera;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Vector3 direction = new Vector3(0, -1, 1).normalized;
        Vector3 direction = new Vector3(0, -1, 3).normalized;
        Ray ray = new Ray(mousePosition, direction);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            if (hitInfo.transform.TryGetComponent(out UnitsDragArea dragArea))
            {
                _units[0].transform.position = hitInfo.point;

                Vector3 newDireciton = Vector3.down;
                newDireciton.z = 0.1f;
                ray = new Ray(hitInfo.point, newDireciton.normalized);
                if (Physics.Raycast(ray, out hitInfo, 1000))
                {
                    if (hitInfo.transform.TryGetComponent(out FallingUnitTrigger fallingUnitTrigger))
                    {
                        if (_placeUnderUnit != null && _placeUnderUnit != fallingUnitTrigger.PlaceUnderTrigger)
                        {
                            _placeUnderUnit.TurnOffHighlight();
                        }

                        _placeUnderUnit = fallingUnitTrigger.PlaceUnderTrigger;
                        _placeUnderUnit.TurnOnHighlight();
                    }
                    else if (_placeUnderUnit != null)
                    {
                        _placeUnderUnit.TurnOffHighlight();
                        _placeUnderUnit = null;
                    }
                }
            }
        }

        //Debug.Log($"Now Unit is at {_unit.transform.position}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_placeUnderUnit != null)
        {
            _placeUnderUnit.TurnOffHighlight();
            _placeUnderUnit = null;
        }

        _units[0].Collider.enabled = true;
        _units[0].Rigidbody.isKinematic = false;
        _unitsAmount--;
        if (_unitsAmount == 0)
        {
            _units.Clear();
            StartCoroutine(WaitForEndOfDisappear());
            //gameObject.SetActive(false);
        }
        else
        {
            _units.RemoveAt(0);
            StartCoroutine(WaitForEndOfAmountAnimation());
            //_unitsAmountDisplay.text = _unitsAmount.ToString();
        }

        //Cursor.SetCursor(_cursorHand, _cursorHandHotSpot, CursorMode.Auto);
        //StartCoroutine(WaitForEndOfCursorDisappear(_cursorDisappearTime));
    }

    //public void Appear()
    //{
    //    StartCoroutine(WaitForEndOfAppear());
    //}

    private IEnumerator WaitForEndOfDisappear()
    {
        _animator.SetTrigger(_disappearAnimationTrigger);
        yield return new WaitForSeconds(_appearDisappearDelay);
        gameObject.SetActive(false);
    }

    //private IEnumerator WaitForEndOfCursorDisappear(float disappearTime)
    //{
    //    float changeTime = disappearTime / _disappearingCursorSteps.Length;

    //    for (int i = 0; i < _disappearingCursorSteps.Length; i++)
    //    {
    //        Cursor.SetCursor(_disappearingCursorSteps[i], _cursorHandHotSpot, CursorMode.Auto);
    //        yield return new WaitForSeconds(changeTime);
    //    }

    //    Cursor.visible = false;
    //}

    //private IEnumerator WaitForEndOfAppear()
    //{
    //    _animator.SetTrigger(_appearAnimationTrigger);
    //    yield return new WaitForSeconds(_appearDisappearDelay);
    //}

    private IEnumerator WaitForEndOfAmountAnimation()
    {
        _animator.SetTrigger(_amountDisplayIncreaseAnimationTrigger);
        yield return new WaitForSeconds(_changeAmountDelay);
        _unitsAmountDisplay.text = _unitsAmount.ToString();
        _animator.SetTrigger(_amountDisplayDecreaseAnimationTrigger);
    }
}
