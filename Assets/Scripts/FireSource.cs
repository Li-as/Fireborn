using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireSource : MonoBehaviour
{
    [SerializeField] private int _difficultyLevel;

    public event UnityAction<int> DifficultyLevelChanged;

    private void Start()
    {
        DifficultyLevelChanged?.Invoke(_difficultyLevel);
    }
}
