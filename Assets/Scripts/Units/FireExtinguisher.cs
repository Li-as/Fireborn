using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FireExtinguisher : MonoBehaviour
{
    [SerializeField] protected UnitsPanel UnitsPanel;
    [SerializeField] protected ParticleSystem FireOnUnitEffect;
    [SerializeField] protected ParticleSystem UnitDeathEffect;
    [SerializeField] protected ParticleSystem UnitHideEffect;
    [SerializeField] protected ParticleSystem WaterEffect;
    [SerializeField] protected ParticleSystem FireExtinguishEndEffect;
    [SerializeField] protected float FailDelay;
    [SerializeField] protected float SuccessDelay;
    [SerializeField] protected float ExtinguishFireEffectDelay;

    public event UnityAction<bool, Unit, PlaceOnFire> ExtinguishHappened;

    public virtual void TryExtinguishPlace(Unit unit, PlaceOnFire place)
    {
        bool isSuccessed = unit.WaterPowerLevel >= place.FireSource.DifficultyLevel;
        ExtinguishHappened?.Invoke(isSuccessed, unit, place);
    }
}
