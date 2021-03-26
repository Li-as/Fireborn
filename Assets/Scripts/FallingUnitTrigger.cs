using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingUnitTrigger : MonoBehaviour
{
    [SerializeField] private PlaceOnFire _placeUnderTrigger;
    [SerializeField] private ParticleSystem _landingEffect;
    [SerializeField] private float _landingEffectDelay;

    private Unit _fallingUnit;

    public PlaceOnFire PlaceUnderTrigger => _placeUnderTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            _fallingUnit = unit;
            StartCoroutine(WaitForLanding(_landingEffectDelay));
        }
    }

    private IEnumerator WaitForLanding(float delay)
    {
        yield return new WaitForSeconds(delay);
        ParticleSystem spawnedEffect = Instantiate(_landingEffect, _fallingUnit.transform.position, Quaternion.identity);
        _fallingUnit.Reset();
        //_fallingUnit.Rigidbody.useGravity = false;
        _fallingUnit.SetDestination(_placeUnderTrigger);

        yield return new WaitForSeconds(spawnedEffect.main.duration);
        Destroy(spawnedEffect.gameObject);
    }
}
