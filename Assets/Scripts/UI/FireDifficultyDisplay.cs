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
                _text.text = "0";
        }
    }
}
