using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(UnitMover))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int WaterPower;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private Transform _startPoint;

    private Rigidbody _rigidbody;
    private UnitMover _mover;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int WaterPowerLevel => WaterPower;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform StartPoint => _startPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<UnitMover>();
    }

    public void Reset()
    {
        transform.position = _startPoint.position;
        transform.rotation = Quaternion.identity;
        _mover.Reset();
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        _mover.SetDestination(desiredPlace);
    }
}
