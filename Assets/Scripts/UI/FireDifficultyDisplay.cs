using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireDifficultyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private FireSource _fireSource;

    private void OnEnable()
    {
        _fireSource.DifficultyLevelChanged += OnDifficultyLevelChanged;
    }

    private void OnDisable()
    {
        _fireSource.DifficultyLevelChanged -= OnDifficultyLevelChanged;
    }

    private void OnDifficultyLevelChanged(int level)
    {
        _text.text = level.ToString();
    }
}
