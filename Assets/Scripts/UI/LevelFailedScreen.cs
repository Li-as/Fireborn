using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class LevelFailedScreen : MonoBehaviour
{
    [SerializeField] private string _appearAnimationTrigger;
    [SerializeField] private string _currentSceneName;

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

    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(_currentSceneName);
    }
}
