using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(UnitMover))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int WaterPower;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _fireEffectSpawnPoint;
    [SerializeField] private Transform _waterEffectSpawnPoint;
    [SerializeField] private FireExtinguisher _extinguisher;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private UnitMover _mover;
    private UnitView _unitView;

    public Sprite Sprite => _sprite;
    public string Name => _name;
    public int WaterPowerLevel => WaterPower;
    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    public Transform StartPoint => _startPoint;
    public UnitView UnitView => _unitView;
    public Transform FireEffectSpawnPoint => _fireEffectSpawnPoint;
    public Transform WaterEffectSpawnPoint => _waterEffectSpawnPoint;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _mover = GetComponent<UnitMover>();
    }

    public virtual void Reset()
    {
        transform.position = _startPoint.position;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _mover.Reset();
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        _mover.SetDestination(desiredPlace);
    }

    public void StartExtinguish(PlaceOnFire place)
    {
        _extinguisher.TryExtinguishPlace(this, place);
    }
}
