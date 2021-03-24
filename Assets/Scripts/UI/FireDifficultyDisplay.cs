using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FireDifficultyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private FireSource _fireSource;
    [SerializeField] private FireExtinguisher[] _fireExtinguishers;
    [SerializeField] private string _failAnimationTrigger;
    [SerializeField] private string _successAnimationTrigger;
    [SerializeField] private string _replaceAnimationTrigger;
    [SerializeField] private float _replaceDelay;
    [SerializeField] private SuccessExtinguishDisplay _successDisplay;
    [SerializeField] private ParticleSystem _successEffect;

    private Animator _animator;

    private void OnEnable()
    {
        _fireSource.DifficultyLevelChanged += OnDifficultyLevelChanged;
        
        foreach (FireExtinguisher extinguisher in _fireExtinguishers)
        {
            extinguisher.ExtinguishHappened += OnExtinguishHappend;
        }
    }

    private void OnDisable()
    {
        _fireSource.DifficultyLevelChanged -= OnDifficultyLevelChanged;

        foreach (FireExtinguisher extinguisher in _fireExtinguishers)
        {
            extinguisher.ExtinguishHappened -= OnExtinguishHappend;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDifficultyLevelChanged(int level)
    {
        _text.text = level.ToString();
    }

    private void OnExtinguishHappend(bool isSuccessed, Unit unit, PlaceOnFire place)
    {
        if (place.FireSource == _fireSource)
        {
            if (isSuccessed == false)
                _animator.SetTrigger(_failAnimationTrigger);
            else
            {
                StartCoroutine(WaitForEndOfSuccess());
            }
        }
    }

    private IEnumerator WaitForEndOfSuccess()
    {
        _animator.SetTrigger(_replaceAnimationTrigger);
        yield return new WaitForSeconds(_replaceDelay);
        _successDisplay.gameObject.SetActive(true);
        _successDisplay.Animator.SetTrigger(_successDisplay.ReplaceAnimationTrigger);
        yield return new WaitForSeconds(_replaceDelay);
        ParticleSystem successEffect = Instantiate(_successEffect, _successDisplay.transform);
        yield return new WaitForSeconds(successEffect.main.duration);
        Destroy(successEffect.gameObject);
        gameObject.SetActive(false);
    }
}
