using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEngineExtinguisher : FireExtinguisher
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

    //private IEnumerator ExtinguishSuccess(float successDelay, Unit unit, PlaceOnFire place)
    //{
    //    ParticleSystem waterEffect = Instantiate(WaterEffect, unit.WaterEffectSpawnPoint);
    //    ParticleSystem.MainModule waterEffectMain = waterEffect.main;
    //    //Quaternion spawnPointRotation = waterEffect.transform.rotation;
    //    //spawnPointRotation.y = unit.WaterEffectSpawnPoint.rotation.y;
    //    //waterEffect.transform.rotation = spawnPointRotation;
    //    waterEffect.Stop();

    //    /// Get all fire effects
    //    List<ParticleSystem> fireSourceEffects = place.FireSource.FireEffects;

    //    while (fireSourceEffects.Count > 0)
    //    {
    //        /// Pick random fire
    //        int fireEffectNumber = Random.Range(0, fireSourceEffects.Count);
    //        ParticleSystem pickedEffect = fireSourceEffects[fireEffectNumber];
    //        /// Apply right rotation to water effect and play it
    //        Vector3 direction = (pickedEffect.transform.position - unit.transform.position).normalized;
    //        waterEffect.transform.rotation = Quaternion.LookRotation(direction);
    //        waterEffectMain.loop = true;
    //        waterEffect.Play();
    //        /// Delay for extinguish
    //        yield return new WaitForSeconds(ExtinguishFireEffectDelay);

    //        waterEffectMain.loop = false;
    //        /// Destroy water and fire effects
    //        ParticleSystem fireExtinguishEndEffect = Instantiate(FireExtinguishEndEffect, pickedEffect.transform.position, Quaternion.identity);
    //        fireSourceEffects.Remove(pickedEffect);
    //        Destroy(pickedEffect.gameObject);
    //        if (fireSourceEffects.Count == 0)
    //        {
    //            /// Run UI animation
    //            base.TryExtinguishPlace(unit, place);
    //        }
    //        yield return new WaitForSeconds(FireExtinguishEndEffect.main.duration);

    //        Destroy(fireExtinguishEndEffect.gameObject);
    //    }

    //    yield return new WaitForSeconds(successDelay);
    //    /// Spawn unit hide effect
    //    ParticleSystem hideEffect = Instantiate(UnitHideEffect, unit.transform.position, Quaternion.identity);
    //    //unit.gameObject.SetActive(false);
    //    unit.Reset();
    //    yield return new WaitForSeconds(hideEffect.main.duration);

    //    Destroy(hideEffect.gameObject);
    //    Destroy(waterEffect.gameObject);
    //}    
    
    private IEnumerator ExtinguishSuccess(float successDelay, Unit unit, PlaceOnFire place)
    {
        ParticleSystem waterEffect = Instantiate(WaterEffect, unit.WaterEffectSpawnPoint);
        ParticleSystem.MainModule waterEffectMain = waterEffect.main;
        waterEffect.Stop();

        List<ParticleSystem> fireSourceEffects = place.FireSource.FireEffects;

        while (fireSourceEffects.Count > 0)
        {
            int fireEffectNumber = Random.Range(0, fireSourceEffects.Count);
            ParticleSystem pickedEffect = fireSourceEffects[fireEffectNumber];
            Vector3 direction = (pickedEffect.transform.position - unit.transform.position).normalized;
            waterEffect.transform.rotation = Quaternion.LookRotation(direction);
            waterEffectMain.loop = true;
            waterEffect.Play();
            yield return new WaitForSeconds(ExtinguishFireEffectDelay);

            waterEffectMain.loop = false;
            ParticleSystem fireExtinguishEndEffect = Instantiate(FireExtinguishEndEffect, pickedEffect.transform.position, Quaternion.identity);
            fireSourceEffects.Remove(pickedEffect);
            Destroy(pickedEffect.gameObject);
            yield return new WaitForSeconds(FireExtinguishEndEffect.main.duration);

            Destroy(fireExtinguishEndEffect.gameObject);
        }

        ParticleSystem hideEffect = Instantiate(UnitHideEffect, unit.transform.position, Quaternion.identity);
        unit.Reset();
        yield return new WaitForSeconds(successDelay);

        base.TryExtinguishPlace(unit, place);
        place.Destroyed.Animator.SetTrigger(place.Destroyed.DisappearAnimationTrigger);
        yield return new WaitForSeconds(ChangePlaceDelay);
        place.Extinguished.gameObject.SetActive(true);
        place.Extinguished.transform.localScale = Vector3.zero;
        place.Extinguished.Animator.SetTrigger(place.Extinguished.AppearAnimationTrigger);
        yield return new WaitForSeconds(ChangePlaceDelay);
        place.Extinguished.Animator.SetTrigger(place.Extinguished.IdleAnimationTrigger);

        place.Destroyed.gameObject.SetActive(false);
        Destroy(hideEffect.gameObject);
        Destroy(waterEffect.gameObject);
    }
}
