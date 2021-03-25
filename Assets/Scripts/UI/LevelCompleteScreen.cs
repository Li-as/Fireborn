using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private string _appearAnimationTrigger;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        _animator.SetTrigger(_appearAnimationTrigger);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnContinueButtonClick()
    {
    }
}
