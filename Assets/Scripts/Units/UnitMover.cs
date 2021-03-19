using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public abstract class UnitMover : MonoBehaviour
{
    [SerializeField] protected float Speed;

    protected bool IsHaveDestination;
    protected PathPoint[] CurrentPath;
    protected Vector3 TargetPosition;
    protected Quaternion TargetRotation;
    protected int TargetPositionIndex;
    protected float MaxDistanceFromTargetPosition;

    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        CurrentPath = desiredPlace.TryGetPath(_unit);
        if (CurrentPath != null && CurrentPath.Length > 0)
        {
            IsHaveDestination = true;
            TargetPositionIndex = 0;
            TargetPosition = CurrentPath[TargetPositionIndex].transform.position;
            Debug.Log($"Start moving to {desiredPlace.transform.name}");
        }
    }
}
