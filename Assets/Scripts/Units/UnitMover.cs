using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] protected float MaxDistanceFromTargetPosition;

    protected bool IsHaveDestination;
    protected PathPoint[] CurrentPath;
    protected Vector3 TargetPosition;
    protected Quaternion TargetRotation;
    protected int TargetPositionIndex;
    protected PlaceOnFire PlaceToExtinguish;

    private Unit _unit;

    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (IsHaveDestination)
        {
            if (IsAtTargetPosition(TargetPosition) == false)
            {
                Vector3 target = TargetPosition;
                target.y = transform.position.y;
                float maxDistanceDelta = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, maxDistanceDelta);

                if (transform.rotation != TargetRotation)
                {
                    float maxDegreesDelta = RotationSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, maxDegreesDelta);
                }
            }
            else if (TargetPosition == CurrentPath[CurrentPath.Length - 1].transform.position)
            {
                IsHaveDestination = false;
                _unit.StartExtinguish(PlaceToExtinguish);
            }
            else
            {
                if (TargetPositionIndex + 1 != CurrentPath.Length)
                {
                    Vector3 target = CurrentPath[TargetPositionIndex + 1].transform.position - CurrentPath[TargetPositionIndex].transform.position;
                    float newRotation = Mathf.Atan2(target.x, target.z) * Mathf.Rad2Deg;
                    Quaternion newTargetRotation = Quaternion.Euler(new Vector3(0, newRotation, 0));
                    if (TargetRotation != newTargetRotation)
                    {
                        TargetRotation = newTargetRotation;
                    }

                    TargetPositionIndex++;
                    TargetPosition = CurrentPath[TargetPositionIndex].transform.position;
                }
            }
        }
    }

    protected virtual bool IsAtTargetPosition(Vector3 targetPosition)
    {
        if ((targetPosition - transform.position).magnitude <= MaxDistanceFromTargetPosition)
        {
            return true;
        }

        return false;
    }

    public void SetDestination(PlaceOnFire place)
    {
        CurrentPath = place.TryGetPath(_unit);
        if (CurrentPath != null && CurrentPath.Length > 0)
        {
            IsHaveDestination = true;
            TargetPositionIndex = 0;
            TargetPosition = CurrentPath[TargetPositionIndex].transform.position;
            PlaceToExtinguish = place;
            Debug.Log($"Start moving to {place.transform.name}");
        }
    }

    public void Reset()
    {
        IsHaveDestination = false;
        CurrentPath = null;
        PlaceToExtinguish = null;
        TargetPosition = _unit.StartPoint.position;
        TargetPositionIndex = 0;
        TargetRotation = Quaternion.identity;
    }
}
