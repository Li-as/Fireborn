using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int WaterPower;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private Transform _startPoint;

    private Rigidbody _rigidbody;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int WaterPowerLevel => WaterPower;
    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetPosition()
    {
        transform.position = _startPoint.position;
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        Debug.Log($"Start moving to {desiredPlace.transform.name}");
    }
}
