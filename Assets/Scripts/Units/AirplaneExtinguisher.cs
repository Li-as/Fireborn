using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneExtinguisher : FireExtinguisher
{
    public override void TryExtinguishPlace(Unit unit, PlaceOnFire place)
    {
        if (place.FireSource.DifficultyLevel > unit.WaterPowerLevel)
        {
            StartCoroutine(ExtinguishFail(FailDelay, unit, place));
        }
        else
        {
            StartCoroutine(ExtinguishSuccess(SuccessDelay, unit, place));
        }
    }

    private IEnumerator ExtinguishFail(float failDelay, Unit unit, PlaceOnFire place)
    {
        /// Spawn fire effect on unit
        /// Run UI animation
        /// Delay
        /// Spawn unit hide effect
        /// Disable unit gameObject
        ParticleSystem fireOnUnitEffect = Instantiate(FireOnUnitEffect, unit.FireEffectSpawnPoint);
        base.TryExtinguishPlace(unit, place);
        yield return new WaitForSeconds(failDelay);

        Destroy(fireOnUnitEffect.gameObject);
        unit.gameObject.SetActive(false);
        ParticleSystem deathEffect = Instantiate(UnitDeathEffect, unit.DeathEffectSpawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(deathEffect.main.duration);

        Destroy(deathEffect.gameObject);
    }

    private IEnumerator ExtinguishSuccess(float successDelay, Unit unit, PlaceOnFire place)
    {
        ParticleSystem waterEffect = Instantiate(WaterEffect, unit.WaterEffectSpawnPoint);
        ParticleSystem.MainModule waterEffectMain = waterEffect.main;

        List<ParticleSystem> fireSourceEffects = place.FireSource.FireEffects;
        List<ParticleSystem> fireExtinguishEndEffects = new List<ParticleSystem>();

        while (unit.IsInRequiredPlace == true)
        {
            if (fireSourceEffects.Count > 0)
            {
                yield return new WaitForSeconds(ExtinguishFireEffectDelay);
                fireExtinguishEndEffects.Add(Instantiate(FireExtinguishEndEffect, fireSourceEffects[0].transform.position, Quaternion.identity));
                Destroy(fireSourceEffects[0].gameObject);
                fireSourceEffects.RemoveAt(0);
            }
            else
            {
                yield return null;
            }
        }

        waterEffectMain.loop = false;

        foreach (ParticleSystem fireSourceEffect in fireSourceEffects)
        {
            fireExtinguishEndEffects.Add(Instantiate(FireExtinguishEndEffect, fireSourceEffect.transform.position, Quaternion.identity));
            Destroy(fireSourceEffect.gameObject);
        }

        fireSourceEffects.Clear();
        base.TryExtinguishPlace(unit, place);
        yield return new WaitForSeconds(FireExtinguishEndEffect.main.duration);

        foreach (ParticleSystem fireExtinguishEndEffect in fireExtinguishEndEffects)
        {
            Destroy(fireExtinguishEndEffect.gameObject);
        }

        fireExtinguishEndEffects.Clear();
        yield return new WaitForSeconds(successDelay);

        ParticleSystem hideEffect = Instantiate(UnitHideEffect, unit.transform.position, Quaternion.identity);
        //unit.gameObject.SetActive(false);
        unit.Reset();
        yield return new WaitForSeconds(hideEffect.main.duration);

        Destroy(hideEffect.gameObject);
        Destroy(waterEffect.gameObject);
    }
}
