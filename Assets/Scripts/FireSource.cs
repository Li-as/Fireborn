using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireSource : MonoBehaviour
{
    [SerializeField] private int _difficultyLevel;
    [SerializeField] private List<ParticleSystem> _fireEffects;

    public event UnityAction<int> DifficultyLevelChanged;
    public int DifficultyLevel => _difficultyLevel;
    public List<ParticleSystem> FireEffects => _fireEffects;

    private void Start()
    {
        DifficultyLevelChanged?.Invoke(_difficultyLevel);
    }
}
