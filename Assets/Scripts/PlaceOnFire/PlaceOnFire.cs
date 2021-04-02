using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class PlaceOnFire : MonoBehaviour
{
    [SerializeField] private FireSource _fireSource;
    [SerializeField] private Transform _firefighterPath;
    [SerializeField] private Transform _fireEnginePath;
    [SerializeField] private Transform _helicopterPath;
    [SerializeField] private Transform _airplanePath;
    [SerializeField] private ParticleSystem[] _highlightEffects;
    [SerializeField] private PlaceOnFire _destroyed;
    [SerializeField] private PlaceOnFire _extinguished;
    [SerializeField] private string _disappearAnimationTrigger;
    [SerializeField] private string _appearAnimationTrigger;
    [SerializeField] private string _idleAnimationTrigger;

    private Animator _animator;

    public FireSource FireSource => _fireSource;
    public PlaceOnFire Destroyed => _destroyed;
    public PlaceOnFire Extinguished => _extinguished;
    public Animator Animator => _animator;
    public string DisappearAnimationTrigger => _disappearAnimationTrigger;
    public string AppearAnimationTrigger => _appearAnimationTrigger;
    public string IdleAnimationTrigger => _idleAnimationTrigger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public PathPoint[] TryGetPath(Unit unit)
    {
        PathPoint[] pathPoints;
        if (unit is Firefighter)
        {
            pathPoints = _firefighterPath.GetComponentsInChildren<PathPoint>();
        }
        else if (unit is FireEngine)
        {
            pathPoints = _fireEnginePath.GetComponentsInChildren<PathPoint>();
        }
        else if (unit is Helicopter)
        {
            pathPoints = _helicopterPath.GetComponentsInChildren<PathPoint>();
        }
        else if (unit is Airplane)
        {
            pathPoints = _airplanePath.GetComponentsInChildren<PathPoint>();
        }
        else
        {
            return null;
        }

        return pathPoints;
    }

    public void TurnOnHighlight()
    {
        foreach (ParticleSystem highlight in _highlightEffects)
        {
            ParticleSystem.MainModule highlightMain = highlight.main;
            highlightMain.loop = true;
            highlight.Play();
        }
    }

    public void TurnOffHighlight()
    {
        foreach (ParticleSystem highlight in _highlightEffects)
        {
            ParticleSystem.MainModule highlightMain = highlight.main;
            highlightMain.loop = false;
        }
    }
}
