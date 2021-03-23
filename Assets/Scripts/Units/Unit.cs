using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int WaterPower;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private GameObject _icon;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleAnimationTrigger;
    [SerializeField] private string _runningAnimationTrigger;
    [SerializeField] private string _firefightingAnimationTrigger;
    //[SerializeField] private string _dyingAnimationTrigger;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _fireEffectSpawnPoint;
    [SerializeField] private Transform _waterEffectSpawnPoint;
    [SerializeField] private Transform _deathEffectSpawnPoint;
    [SerializeField] private FireExtinguisher _extinguisher;

    private Collider _collider;
    private Rigidbody _rigidbody;
    private UnitMover _mover;
    private UnitView _unitView;
    private PlaceOnFire _requiredPlace;
    private bool _isInRequiredPlace;

    public Sprite Sprite => _sprite;
    public GameObject Icon => _icon;
    public string Name => _name;
    public int WaterPowerLevel => WaterPower;
    public Collider Collider => _collider;
    public Rigidbody Rigidbody => _rigidbody;
    public Animator Animator => _animator;
    public string IdleAnimationTrigger => _idleAnimationTrigger;
    public string RunningAnimationTrigger => _runningAnimationTrigger;
    public string FirefightingAnimationTrigger => _firefightingAnimationTrigger;
    //public string DyingAnimationTrigger => _dyingAnimationTrigger;
    public Transform StartPoint => _startPoint;
    public UnitView UnitView => _unitView;
    public bool IsInRequiredPlace => _isInRequiredPlace;
    public Transform FireEffectSpawnPoint => _fireEffectSpawnPoint;
    public Transform WaterEffectSpawnPoint => _waterEffectSpawnPoint;
    public Transform DeathEffectSpawnPoint => _deathEffectSpawnPoint;

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
        _requiredPlace = null;
    }

    public void SetDestination(PlaceOnFire desiredPlace)
    {
        _mover.SetDestination(desiredPlace);
        _requiredPlace = desiredPlace;
    }

    public void StartExtinguish(PlaceOnFire place)
    {
        _extinguisher.TryExtinguishPlace(this, place);
    }

    public void EnterPlace(PlaceOnFire place)
    {
        if (_requiredPlace == place)
        {
            _isInRequiredPlace = true;
            StartExtinguish(place);
        }
    }

    public void ExitPlace(PlaceOnFire place)
    {
        if (_requiredPlace == place)
        {
            _isInRequiredPlace = false;
            _requiredPlace = null;
        }
    }
}
