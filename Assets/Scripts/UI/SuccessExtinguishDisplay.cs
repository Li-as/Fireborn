using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SuccessExtinguishDisplay : MonoBehaviour
{
    [SerializeField] private string _replaceAnimationTrigger;
    
    private Animator _animator;

    public Animator Animator => _animator;
    public string ReplaceAnimationTrigger => _replaceAnimationTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
