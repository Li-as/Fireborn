using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndHandler : MonoBehaviour
{
    [SerializeField] private int _amountOfUnits;
    [SerializeField] private FireExtinguisher[] _extinguishers;
    [SerializeField] private LevelCompleteScreen _completeScreen;
    [SerializeField] private LevelFailedScreen _failedScreen;
    [SerializeField] private float _resultsScreenDelay;

    private int _amountOfSuccessExtinguishes;
    private int _amountOfFailExtinguishes;

    private void OnEnable()
    {
        foreach (FireExtinguisher extinguisher in _extinguishers)
        {
            extinguisher.ExtinguishHappened += OnExtinguishHappened;
        }
    }

    private void OnDisable()
    {
        foreach (FireExtinguisher extinguisher in _extinguishers)
        {
            extinguisher.ExtinguishHappened -= OnExtinguishHappened;
        }
    }

    private void OnExtinguishHappened(bool isSuccessed, Unit unit, PlaceOnFire place)
    {
        if (isSuccessed)
        {
            _amountOfSuccessExtinguishes++;
        }
        else
        {
            _amountOfFailExtinguishes++;
        }

        if (_amountOfFailExtinguishes + _amountOfSuccessExtinguishes == _amountOfUnits)
        {
            StartCoroutine(ShowGameResults());
        }
    }

    private IEnumerator ShowGameResults()
    {
        yield return new WaitForSeconds(_resultsScreenDelay);

        if (_amountOfSuccessExtinguishes == _amountOfUnits)
        {
            _completeScreen.gameObject.SetActive(true);
            _completeScreen.Show();
        }
        else
        {
            _failedScreen.gameObject.SetActive(true);
            _failedScreen.Show();
        }
    }
}
