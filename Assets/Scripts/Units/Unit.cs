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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<UnitMover>();
    }

    public void ResetPosition()
    {
        transform.position = _startPoint.position;
        transform.rotation = Quaternion.identity;
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        _mover.SetDestination(desiredPlace);
    }
}
